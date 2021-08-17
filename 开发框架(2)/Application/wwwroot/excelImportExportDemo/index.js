const App = {
    data() {
        return {
            fileList: [],
            file2: [],
            excelDownPath:""
        };
    },
    methods: {
        //触发服务端上传
        submitUpload: function () {
            this.$refs.upload.submit();
        },
        //文件上传服务端失败时的钩子
        fileUploadFail: function (err, file, fileList) {
            console.log("文件上传失败", file, fileList);
        },
        //文件上传服务端成功时的钩子
        fileUploadSuccess: function (response, file, fileList) {
            if (response.resultInfo.isSuccess == 1) {
                this.$message({
                    message: "上传成功",
                    type: "success"
                });
            }
            else {
                this.$message.error(response.resultInfo.errorInfo);
            }
        },
        //文件上传时的变化
        fileChange: function (file, fileList) {
            this.fileList = [];
            this.fileList.push(file);
        },
        //文件移除
        handleRemove: function (file, fileList) {
            this.fileList = [];
        },
        exportExcel: function () {
        //导出EXCEL
            axios.post("/api/ExcelImportExportDemoApi/ExportExcel"
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.excelDownPath = res.downLoadPath;
                }
                else {
                    this.$message({
                        duration: 1000,
                        type: 'info',
                        message: res.resultInfo.errorInfo
                    });
                }
            }).catch(function (error) {
                // 请求失败处理
                console.log(error);
            });
        }
    },
    created: function () {

    }
};

const app = Vue.createApp(App);
app.use(ElementPlus);
app.mount("#app");