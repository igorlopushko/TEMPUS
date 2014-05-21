window.App = new Backbone.Marionette.Application();

App.module('Project.Plan', function (ProjectPlan, App, Backbone, Marionette, $, _) {

    App.addRegions({
        list: '#tasksList',
        gantt: '#tasksGantt',
        modal: ModalRegion
    });

    App.commands.setHandler('showCreateTask', function () {
        var task = new ProjectPlan.Task(),
            view = new ProjectPlan.EditTaskModalView({model: task});
        App.modal.show(view);
    });

    App.commands.setHandler('showEditTask', function (task) {
        App.modal.show(new ProjectPlan.EditTaskModalView({model: task}));
    });

    App.commands.setHandler('showDeleteTask', function (task) {
        App.modal.show(new ProjectPlan.DeleteTaskModalView({model: task}));
    });

    this.Task = Backbone.Model.extend({
        localStorage: new Backbone.LocalStorage('tasks')
    });

    this.Tasks = Backbone.Collection.extend({
        model: this.Task,

        localStorage: new Backbone.LocalStorage('tasks')
    });

    this.TaskView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#taskTemplate'),

        tagName: 'tr',

        events: {
            'click .edit-task': 'showEditTask',
            'click .delete-task': 'showDeleteTask'
        },

        initialize: function () {
            this.model.on('change', this.render);
        },

        showEditTask: function (e) {
            e.preventDefault();
            App.commands.execute('showEditTask', this.model);
        },

        showDeleteTask: function (e) {
            e.preventDefault();
            App.commands.execute('showDeleteTask', this.model);
        }
    });

    this.EmptyTasksView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#emptyTasksTemplate'),

        tagName: 'tr',
    });

    this.TasksView = Backbone.Marionette.CompositeView.extend({
        itemView: this.TaskView,

        emptyView: this.EmptyTasksView,

        itemViewContainer: 'tbody',

        template: Backbone.Marionette.TemplateCache.get('#tasksTemplate'),

        events: {
            'click .create-task': 'showCreateTask'
        },

        showCreateTask: function (e) {
            e.preventDefault();
            App.commands.execute('showCreateTask');
        }
    });

    this.EditTaskModalView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#editTaskModalTemplate'),

        className: 'modal-dialog',

        events: {
            'submit form': 'saveTask'
        },

        serializeData: function () {
            return {item: this.model.toJSON()};
        },

        saveTask: function (e) {
            e.preventDefault();

            var data = this.$('form').serializeObject();

            if (!data['start_date']) {
                data['start_date'] = null;
            } else {
                data['start_date'] = moment(data['start_date'], 'MM/DD/YYYY').toDate();
            }

            if (!data['due_date']) {
                data['due_date'] = null;
            } else {
                data['due_date'] = moment(data['due_date'], 'MM/DD/YYYY').toDate();
            }

            if (!data['planned_hours']) {
                data['planned_hours'] = null;
            }

            this.model.set(data);

            var isNew = this.model.isNew();
            this.model.save().done(_.bind(this.onTaskSave, this, isNew));
        },

        onTaskSave: function (isNew) {
            if (isNew) {
                App.vent.trigger('taskCreated', this.model);
            }
            this.close();
        },

        onRender: function () {
            this.$('[role="datepicker"]').datetimepicker({
                pickTime: false
            });
        }
    });

    this.DeleteTaskModalView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#deleteTaskModalTemplate'),

        className: 'modal-dialog',

        events: {
            'click .delete-task': 'deleteTask'
        },

        deleteTask: function (e) {
            e.preventDefault();
            this.model.destroy().done(_.bind(this.close, this));
        }
    });

});
