import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '../views/Home.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/single-events',
    name: 'SingleEvents',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/SingleEvents.vue')
  },
  {
    path: '/events',
    name: 'Events',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/events/EventsWrapper.vue'),
    children: [
      {
        path: '',
        component: () => import('../views/events/EventsIndex.vue')
      },
      {
        path: 'create',
        component: () => import('../views/events/EventsDetail.vue'),
        props: { editMode: false }
      },
      {
        path: ':place',
        component: () => import('../views/events/EventsIndex.vue'),
        props: route => ({ placeCode: route.params.place })
      },
      {
        path: ':place/:id',
        component: () => import('../views/events/EventsDetail.vue'),
        props: route => ({ editMode: true, placeCode: route.params.place, eventInstanceId: parseInt((route.params.id as string)) })
      }
    ]
  },
  {
    path: '/places',
    name: 'Places',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/places/PlacesWrapper.vue'),
    children: [
      {
        path: '',
        component: () => import('../views/places/PlacesIndex.vue')
      },
      {
        path: ':code',
        component: () => import(/* webpackChunkName: "about" */ '../views/places/PlacesDetail.vue')
      }
    ]
  },
  // {
  //   path: '/places/:code',
  //   name: 'PlacesDetail',
  //   component: () => import('../views/places/PlacesDetail.vue')
  // },
  {
    path: '/categories',
    name: 'Categories',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Categories.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
