const App = {
    data() {
        let validRoleName = (rule, value, callback) => {
            axios.post("/api/SystemManage/RoleApi/ValidateRoleName",
                {
                    roleName: value,
                    roleId: this.roleInfo.roleId
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
        let validateRoleCode = (rule, value, callback) => {
            axios.post("/api/SystemManage/RoleApi/ValidateRoleCode",
                {
                    roleName: value,
                    roleId: this.roleInfo.roleId
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
        return {
            treeData: [],
            searchName: "",
            riqi:"",
            //riqi1:"",
            total: 1000,
            pageSize: 10,
            roleListInfo: [],
            currentPage: 1,
            editFormDialog: false,
            roleInfo: {},
            selectMenus: [],
            rules: {
                sort: [
                    { required: true, message: '请输入等级', trigger: 'blur' }
                ],
                roleName: [
                    { required: true, message: '请输入成员名称', trigger: 'blur' },
                    { validator: validRoleName, trigger: 'blur' }
                ],
                roleCode: [
                    { required: true, message: '请输入成员地位', trigger: 'blur' },
                    { validator: validateRoleCode, trigger: 'blur' }
                ]
            },
            defaultProps: {
                children: 'children',
                label: 'label'
            }
        };
    },
    methods: {
        search: function (pageIndex) {
            this.count();
            this.currentPage = pageIndex;
            //当把最后一页的数据删完时，页索引调至前一页
            if (this.total % this.pageSize == 0 && parseInt(this.total / this.pageSize) + 1 == pageIndex && pageIndex != 1) {
                pageIndex = pageIndex - 1;
                this.currentPage = pageIndex;
            }
            this.getList(pageIndex);
        },
        count: function () {
            axios.post("/api/SystemManage/RoleApi/RoleCount",
                {
                    roleName: this.searchName
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.total = res.recordCount;
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
        },
        getList: function (pageIndex) {
            axios.post("/api/SystemManage/RoleApi/RoleList",
                {
                    roleName: this.searchName,
                    pageIndex: pageIndex,
                    pageSize: this.pageSize
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.roleListInfo = res.listRoleManage;
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
        },
        handleCurrentChange: function (val) {
            this.currentPage = val;
            this.search(this.currentPage);
        },
        addEdit: async function (id) {
            //角色信息
            await axios.post("/api/SystemManage/RoleApi/RoleInfo",
                {
                    roleId: id
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                //注意项，需先初始化begin
                this.$nextTick(() => {
                    this.$refs['roleInfo'].resetFields();
                });
                //注意项，需先初始化end
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.roleInfo = res.roleAndMenuInfo.roleManage;
                    //this.$nextTick(() => {
                    if (res.roleAndMenuInfo.listRoleMenu.length != 0) {
                        this.selectMenus = res.roleAndMenuInfo.listRoleMenu;
                    }
                    else {
                        this.selectMenus = [];
                    }
                    //});
                    this.roleInfo.roleId = id;
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

            //加载树
            await axios.post("/api/SystemManage/FuncManageApi/ListTree", {},
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
        save: function () {
            var roleMenus = this.$refs.tree.getCheckedKeys();
            this.$refs['roleInfo'].validate((valid) => {
                if (valid) {
                    axios.post("/api/SystemManage/RoleApi/SaveRoleInfo",
                        {
                            roleManage: this.roleInfo,
                            listRoleMenus: roleMenus
                        },
                        {
                            headers: {
                                Authorization: window.localStorage.getItem("storToken")
                            }
                        }
                    ).then(response => {
                        var res = response.data;
                        if (res.resultInfo.isSuccess == 1) {
                            this.$alert('角色编辑成功', '消息', {
                                confirmButtonText: '确定',
                                type: 'success',
                                callback: action => {
                                    this.editFormDialog = false;
                                    this.search(this.currentPage);
                                }
                            });
                        }
                        else {
                            this.$message({
                                duration: 1000,
                                type: 'warning',
                                message: res.resultInfo.errorInfo
                            });
                            console.log(res.resultInfo.errorInfo);
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
        delRole: function (id, roleName) {
            this.$confirm('确实要删除【' + roleName + '】?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                cancelButtonClass: 'btn-custom-cancel',
                type: 'warning'
            }).then(() => {
                axios.post("/api/SystemManage/RoleApi/RoleDelete",
                    {
                        roleId: id
                    },
                    {
                        headers: {
                            Authorization: window.localStorage.getItem("storToken")
                        }
                    }
                ).then(response => {
                    var res = response.data;
                    if (res.resultInfo.isSuccess == 1) {
                        this.search(this.currentPage);
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
        dateFormat: function (updateTime) {
            return ((updateTime + "").replace("T", " ")).substr(0, 16);
        }
    },
    created: function () {
        this.search(1);
    }
};

const app = Vue.createApp(App);
app.use(ElementPlus);
app.mount("#app");