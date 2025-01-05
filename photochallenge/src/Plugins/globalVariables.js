export default {
    install(app, options) {
        app.config.globalProperties.$globalVariables = options;
    }
};
