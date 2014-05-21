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

    this.GanttItemView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#ganttItemTemplate'),

        tagName: 'tr',

        initialize: function (options) {
            this.minDate = options.minDate;
            this.maxDate = options.maxDate;
        },

        serializeData: function () {
            var columnWidth = 52;
            return {
                isVisible: this.model.get('start_date') !== null && this.model.get('due_date') !== null,
                size: this.maxDate.diff(this.minDate, 'days'),
                width: columnWidth * moment(this.model.get('due_date')).diff(moment(this.model.get('start_date')), 'days'),
                left: columnWidth * moment(this.model.get('start_date')).diff(this.minDate, 'days')
            }
        }
    });

    this.GanttView = Backbone.Marionette.CompositeView.extend({
        template: Backbone.Marionette.TemplateCache.get('#ganttTemplate'),

        itemView: this.GanttItemView,

        className: 'gantt-container',

        itemViewContainer: 'tbody',

        events: {
            'mousedown .gantt': 'startDragging',
            'mousemove .gantt': 'drag',
            'mouseleave .gantt': 'stopDragging',
            'mouseup .gantt': 'stopDragging'
        },

        ui: {
            'gantt': '.gantt',
            'ganttTable': '.gantt table'
        },

        initialize: function () {
            this.collection.on('add remove reset change', this.render);
        },

        itemViewOptions: function (model, index) {
            return {
                minDate: this.getStartDate(),
                maxDate: this.getFinishDate()
            };
        },

        startDragging: function (e) {
            this.dragPoint = e.screenX;
        },

        drag: function (e) {
            if (this.dragPoint) {
                var offset = this.dragPoint - e.screenX,
                    left = parseInt(this.ui.gantt.css('left'), 10),
                    maxLeft = 0,
                    minLeft = this.ui.gantt.width() - this.ui.ganttTable.width(),
                    newLeft = left - offset;

                if (newLeft > maxLeft) {
                    newLeft = maxLeft;
                }

                if (newLeft < minLeft) {
                    newLeft = minLeft;
                }

                this.ui.gantt.css('left', newLeft + 'px');
                this.dragPoint = e.screenX;
            }
        },

        stopDragging: function (e) {
            this.dragPoint = null;
        },

        getStartDate: function () {
            var start = _.min(this._getDates('start_date'));

            if (!start) {
                start = new Date();
            }

            return moment(start).subtract('days', 7);
        },

        getFinishDate: function () {
            var finish = _.max(this._getDates('due_date'));

            if (!finish) {
                finish = new Date();
            }

            return moment(finish).add('days', 7);
        },

        _getDates: function (attribute) {
            return _.map(
                _.filter(
                    this.collection.pluck(attribute),
                    function (val) {
                        return val !== null;
                    }
                ),
                function (val) {
                    return moment(val).toDate();
                }
            );
        },

        serializeData: function () {
            var start = this.getStartDate(),
                finish = this.getFinishDate();

            days = []
            while (start.unix() < finish.unix()) {
                days.push(start);
                start = start.clone().add('days', 1);
            }
            return {days: days};
        },

    });

});
