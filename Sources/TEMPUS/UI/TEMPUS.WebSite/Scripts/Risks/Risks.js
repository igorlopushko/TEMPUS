window.App = new Backbone.Marionette.Application();

App.module('Risks', function (Risks, App, Backbone, Marionette, $, _) {

    App.addRegions({
        list: '#risksList',
        modal: ModalRegion
    });

    App.commands.setHandler('showCreateRisk', function () {
        var risk = new Risks.Risk(),
            view = new Risks.EditRiskModalView({ model: risk });
        App.modal.show(view);
    });

    App.commands.setHandler('showEditRisk', function (risk) {
        App.modal.show(new Risks.EditRiskModalView({ model: risk }));
    });

    App.commands.setHandler('showDeleteRisk', function (risk) {
        App.modal.show(new Risks.DeleteRiskModalView({ model: risk }));
    });

    this.Risk = Backbone.Model.extend({
        localStorage: new Backbone.LocalStorage('risks')
    });

    this.Risks = Backbone.Collection.extend({
        model: this.Risk,

        localStorage: new Backbone.LocalStorage('risks')
    });

    jQuery(".create-risk").click(function () {
        App.commands.execute('showCreateRisk');
    });

    this.RiskView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#riskTemplate'),

        tagName: 'tr',

        events: {
            'click .edit-risk': 'showEditRisk',
            'click .delete-risk': 'showDeleteRisk'
        },

        initialize: function () {
            this.model.on('change', this.render);
            var inside = this.model;
        },

        showEditRisk: function (e) {
            e.preventDefault();
            App.commands.execute('showEditRisk', this.model);
        },

        showDeleteRisk: function (e) {
            e.preventDefault();
            App.commands.execute('showDeleteRisk', this.model);
        }
    });

    this.EmptyRisksView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#emptyRisksTemplate'),

        tagName: 'tr',
    });

    this.RisksView = Backbone.Marionette.CompositeView.extend({
        itemView: this.RiskView,

        emptyView: this.EmptyRisksView,

        itemViewContainer: 'tbody',

        template: Backbone.Marionette.TemplateCache.get('#risksTemplate'),

        events: {
            'click .create-risk': 'showCreateRisk'
        },

        showCreateRisk: function (e) {
            e.preventDefault();
            App.commands.execute('showCreateRisk');
        }
    });

    this.EditRiskModalView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#editRiskModalTemplate'),

        className: 'modal-dialog',

        events: {
            'submit form': 'saveRisk'
        },

        serializeData: function () {
            return {item: this.model.toJSON()};
        },

        saveRisk: function (e) {
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
            this.model.save().done(_.bind(this.onRiskSave, this, isNew));
        },

        onRiskSave: function (isNew) {
            if (isNew) {
                App.vent.trigger('riskCreated', this.model);
            }
            this.close();
        },

        onRender: function () {
            this.$('[role="datepicker"]').datetimepicker({
                pickTime: false
            });
        }
    });

    this.DeleteRiskModalView = Backbone.Marionette.ItemView.extend({
        template: Backbone.Marionette.TemplateCache.get('#deleteRiskModalTemplate'),

        className: 'modal-dialog',

        events: {
            'click .delete-risk': 'deleteRisk'
        },

        deleteRisk: function (e) {
            e.preventDefault();
            this.model.destroy().done(_.bind(this.close, this));
        }
    });

});
