<template>
  <div class="space-y-4">
    <NuxtLink to="/jjb-check" class="flower-link">JJB检测</NuxtLink>
  </div>
</template>

<script setup>
import { ref } from "vue";
import ClipboardJS from "clipboard";
</script>

<style scoped>
.flower-link {
  display: inline-block; /* 使链接成为块级元素，便于样式调整 */
  padding: 10px 20px; /* 内边距 */
  border-radius: 25px; /* 圆角 */
  background-color: #f39c12; /* 背景颜色 */
  color: #fff; /* 字体颜色 */
  text-decoration: none; /* 去掉下划线 */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* 阴影效果 */
  transition: background-color 0.3s, transform 0.3s; /* 动画效果 */
}

.flower-link:hover {
  background-color: #e67e22; /* 悬停时改变背景颜色 */
  transform: translateY(-2px); /* 悬停时上移 */
}
</style>
