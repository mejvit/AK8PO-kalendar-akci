import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './assets/css/main.css'
import 'bootstrap-icons/font/bootstrap-icons.css'

createApp(App).use(router).mount('#app')
