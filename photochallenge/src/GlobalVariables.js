export default {
    install: (app, options) => {
        app.config.globalProperties.$serverAddress = options.serverAddress;    }
};