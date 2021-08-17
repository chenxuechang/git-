const App = {
    data() {
        //菜单名称验证
        let validMenuName = (rule, value, callback) => {
            axios.post("/api/SystemManage/FuncManageApi/MenuNameExist",
                {
                    menuName: value,
                    parentId: this.parentId,
                    menuId: this.menuId
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    callback();
                }
                else {
                    callback(new Error(res.resultInfo.errorInfo))
                }
            }).catch(function (error) {
                // 请求失败处理
                console.log(error);
            });
        };
        //菜单编码验证
        let validMenuCode = (rule, value, callback) => {
            axios.post("/api/SystemManage/FuncManageApi/MenuCodeExist",
                {
                    menuCode: value,
                    menuId: this.menuId
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")//storToken存储标识
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    callback();
                }
                else {
                    callback(new Error(res.resultInfo.errorInfo))
                }
            }).catch(function (error) {
                // 请求失败处理
                console.log(error);
            });
        };
        return {
            treeData: [],
            defaultProps: {
                children: 'children',
                label: 'label'
            },
            title: "功能管理",
            //父级菜单ID
            parentId: "00000000-0000-0000-0000-000000000000",
            //当前菜单ID（新增时为00000000-0000-0000-0000-000000000000)
            menuId: "00000000-0000-0000-0000-000000000000",
            menuListInfo: [],
            //单体菜单信息
            menuInfo: {},
            editFormDialog: false,
            //分类列表
            typeOptions:
                [{
                    value: '1',
                    label: '目录'
                }, {
                    value: '2',
                    label: '功能'
                    }],
            typeValue:'1',
            rules: {
                menuName: [//功能管理添加区域//提示区
                    { required: true, message: '请输入名称', trigger: 'blur' },
                    { validator: validMenuName, trigger: 'blur' }
                ],
                menuCode: [
                    { required: true, message: '请输入编码', trigger: 'blur' },
                    { validator: validMenuCode, trigger: 'blur' }
                ],
                menuSort: [
                    { required: true, message: '请输入排序号', trigger: 'blur' }
                ]
            }
        };
    },
    methods: {
        menuTree: function () {
            axios.post("/api/SystemManage/FuncManageApi/ListTree", {},
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.treeData = res.listTree;
                }
                else {
                    this.$message({
                        duration: 1000,
                        type: 'warning',
                        message: res.resultInfo.errorInfo
                    });
                }
            }).catch(function (error) {
                // 请求失败处理
                console.log(error);
            });
        },
        handleNodeClick: function (data) {
            this.menuList(data.id);
            this.parentId = data.id;
            this.title = data.label;
        },
        menuList: function (pid) {
            axios.post("/api/SystemManage/FuncManageApi/ListMenuInfo",
                {
                    parentId: pid
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.menuListInfo = res.listMenu;
                }
                else {
                    this.$message({
                        duration: 1000,
                        type: 'warning',
                        message: res.resultInfo.errorInfo
                    });
                }
            }).catch(function (error) {
                // 请求失败处理
                console.log(error);
            });
        },
        addEdit: function (id) {
            this.menuId = id;
            this.typeValue = "1";
            axios.post("/api/SystemManage/FuncManageApi/MenuSingleInfo",
                {
                    menuId: id
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                //注意项，需先初始化begin
                this.$nextTick(() => {
                    this.$refs['menuInfo'].resetFields();
                });
                //注意项，需先初始化end
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.menuInfo = res.menuInfo;
                    this.menuInfo.menuId = id;
                    this.menuInfo.menuPid = this.parentId;
                    if (id != "00000000-0000-0000-0000-000000000000") {
                        this.typeValue = this.menuInfo.menuType;
                    }
                    this.editFormDialog = true;
                }
                else {
                    this.$message({
                        duration: 1000,
                        type: 'warning',
                        message: res.resultInfo.errorInfo
                    });
                }
            }).catch(function (error) {
                // 请求失败处理
                console.log(error);
            });
        },
        save: function () {
            this.menuInfo.menuType = this.typeValue;
            this.$refs['menuInfo'].validate((valid) => {
                if (valid) {
                    axios.post("/api/SystemManage/FuncManageApi/MenuSave",
                        {
                            menuInfo: this.menuInfo
                        },
                        {
                            headers: {
                                Authorization: window.localStorage.getItem("storToken")
                            }
                        }
                    ).then(response => {
                        var res = response.data;
                        if (res.resultInfo.isSuccess == 1) {
                            this.$alert('菜单编辑成功', '消息', {
                                confirmButtonText: '确定',
                                type: 'success',
                                callback: action => {
                                    this.editFormDialog = false;
                                    this.menuList(this.parentId);
                                    this.menuTree();
                                }
                            });
                        }
                        else {
                            this.$message({
                                duration: 1000,
                                type: 'warning',
                                message: res.resultInfo.errorInfo
                            });
                        }
                    }).catch(function (error) {
                        // 请求失败处理
                        console.log(error);
                    });
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        },
        delMenu: function (menuId, menuName) {
            this.$confirm('确实要删除【' + menuName + '】?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                cancelButtonClass: 'btn-custom-cancel',
                type: 'warning'
            }).then(() => {
                axios.post("/api/SystemManage/FuncManageApi/MenuDelete",
                    {
                        menuId: menuId
                    },
                    {
                        headers: {
                            Authorization: window.localStorage.getItem("storToken")
                        }
                    }
                ).then(response => {
                    var res = response.data;
                    if (res.resultInfo.isSuccess == 1) {
                        this.menuList(this.parentId);
                        this.menuTree();
                        this.$message({
                            duration: 1000,
                            type: 'success',
                            message: "删除成功"
                        });
                    }
                    else {
                        this.$message({
                            duration: 1000,
                            type: 'warning',
                            message: res.resultInfo.errorInfo
                        });
                    }
                }).catch(function (error) {
                    // 请求失败处理
                    console.log(error);
                });
            }).catch(() => {
                this.$message({
                    duration: 1000,
                    type: 'info',
                    message: '取消删除'
                });
            });
        },
        menuIcon: function (className) {
            return '<span class="' + className + '"></span>';
        }
    },
    created: function () {
        this.menuList(this.parentId)
        this.menuTree();
    }
};

const app = Vue.createApp(App);
app.use(ElementPlus);
app.mount("#app");


