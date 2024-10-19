<template>
  <el-input
    v-model="pasteText"
    style="width: 200px"
    type="textarea"
    :rows="1"
    resize="none"
  />
  <el-button @click="parseData" type="primary" :loading="parseLoading"
    >解析数据</el-button
  >
  <el-button @click="pasteText = ''" type="primary" plain>清空</el-button>
  <!-- <el-col :span="24">
    <el-table :data="list" border style="width: 100%">
      <el-table-column prop="date" label="Date" width="180" />
      <el-table-column prop="name" label="Name" width="180" />
      <el-table-column prop="address" label="Address" />
    </el-table>
  </el-col> -->
  <div v-if="errContentList.length > 0" style="margin-top: 30px">
    <!-- <div>
      <el-checkbox v-model="hideOnlyGreen" label="隐藏自行检测（隐藏只包含自行检测的错误）" size="large" />
    </div> -->
    <el-text tag="b">错误列表：</el-text>
    <el-tabs class="demo-tabs" v-model="tabsActive">
      <el-tab-pane
        :label="item.title"
        :name="item.title"
        v-for="(item, index) in errContentList"
        :key="index"
      >
        <el-card>
          <template #header>
            <div class="card-header">
              <span>{{ item.title }}</span>
            </div>
          </template>
          <p v-for="(o, index) in item.checkList" :key="index">
            {{ index + 1 }}：<span v-html="o" />
          </p>
        </el-card>
      </el-tab-pane>
    </el-tabs>
    <!--<div style="font-size: 20px; color: #8b0000;">若发现检测异常，请通过 xz zz 向上反馈</div>-->
    <div style="font-size: 20px; color: #8b0000;">若CD类型为 家族/亲戚，网络/通信，知人（朋友/前后辈/同事），请zz自行检测CD类型是否添加批注，如添加请忽略，若没有添加，请参考范例添加</div>
  </div>
  <div v-if="success" style="margin-top: 30px">
    <div style="font-size: 20px; color: #28a745;">全部检查正确</div>
    <div style="font-size: 20px; color: #8b0000;">若CD类型为 家族/亲戚，网络/通信，知人（朋友/前后辈/同事），请zz自行检测CD类型是否添加批注，如添加请忽略，若没有添加，请参考范例添加</div>
  </div>
</template>

<script setup>
import { ElMessage, ElMessageBox } from "element-plus";
import { ref, computed } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

const hideOnlyGreen = ref(false);

const errContentList = ref([]);

const tabsActive = ref("");

// const filteredErrContentList = computed(() => {
//   if (!hideOnlyGreen.value) {
//     return errContentList.value;
//   }
//   return errContentList.value.filter(item => {
//   // 检查是否所有的 `checkList` 元素都包含 "请zz自行检测是否"
//     return item.checkList.some(
//       o => !o.includes('请zz自行检测是否')
//     );
//   });
// });

const list = ref([]);

const parseLoading = ref(false);

const pasteText = ref("");

const success = ref(false);

const parseData = async () => {
  parseLoading.value = true;
  success.value = false;

  const res = await fetchApi("/jjb/checkData", {
    method: "post",
    body: splitText(pasteText.value),
    onResponse: (_ctx) => _ctx.response,
  });

  parseLoading.value = false;

  if (res.code == 200) {
    errContentList.value = res.data;
    if (res.data.length > 0) {
      success.value = false;
      tabsActive.value = res.data[0].title;
    } else {
      success.value = true;
    }
    pasteText.value = "";
  } else {
    ElMessageBox.alert(res.errMsg, "错误");
  }
};

function splitText(input) {
  // 按行分割
  const lines = input.split("\n");

  // 初始化列数组
  const columns = [];

  // 遍历每一行
  lines.forEach((line) => {
    // 按制表符分割
    const cells = line.split("\t");

    // 将每个单元格添加到对应的列中
    cells.forEach((cell, index) => {
      if (!columns[index]) {
        columns[index] = []; // 初始化列
      }
      columns[index].push(cell); // 添加单元格到列
    });
  });

  return columns;
}

onMounted(() => {});
</script>

<style scoped>
.waterfall-container {
  display: grid;
  grid-template-columns: repeat(
    auto-fill,
    minmax(200px, 1fr)
  ); /* 可调整的列宽 */
  gap: 16px; /* 卡片之间的间距 */
}

.waterfall-card {
  display: flex;
  flex-direction: column;
  justify-content: center; /* 使内容垂直居中 */
  overflow: hidden; /* 隐藏溢出内容 */
}

.card-content {
  padding: 16px; /* 卡片内容内边距 */
}
</style>
