// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  runtimeConfig: {
    public: {
      env: process.env.NUXT_PUBLIC_ENV,
      api: {
        baseUrl: process.env.NUXT_PUBLIC_API_BASEURL,
      },
    },
  },
  app: {
    head: {
      meta: [
        {
          name: "viewport",
          content:
            "width=device-width,initial-scale=1,minimum-scale=1,user-scalable=no",
        },
      ],
    },
  },
  css: ["~/assets/css/main.css"],
  plugins: ["~/plugins/element-plus.js"],
  // nitro: {
  //   // 用于客户端代理
  //   devProxy: {
  //     "/api": {
  //       target: "http://op13429gf933.vicp.fun:53534", // 这里是接口地址
  //       changeOrigin: true,
  //       prependPath: false,
  //     },
  //   }
  // },
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },
  vue: {
    compilerOptions: {
      whitespace: "preserve",
    },
  },
  render: {
    // Disable HTML minification
    minify: false,
  },
});
