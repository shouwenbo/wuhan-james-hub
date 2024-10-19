<template>
  <div class="common-layout">
    <el-container>
      <el-header>
      </el-header>
      <el-main>
        <slot />
      </el-main>
      <el-footer></el-footer>
    </el-container>
  </div>
</template>

<script setup></script>

<style scoped>
.w_1200 {
  width: 1200px;
  margin: 0px auto;
}
</style>
