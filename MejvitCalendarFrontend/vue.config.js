const path = require('path')
const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  outputDir: path.resolve(__dirname, '../MejvitCalendarBackend/wwwroot'),
  assetsDir: './assets',
  transpileDependencies: true,
  css: {
    loaderOptions: {
      scss: {
        additionalData: `
          @import "@/assets/scss/main.scss";
        `,
        implementation: require('node-sass')
      }
    }
  }
})
