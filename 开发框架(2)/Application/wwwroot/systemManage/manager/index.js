const App = {
    data() {
        let validAccout = (rule, value, callback) => {
            let reg = /^(?![0]+$)/
            if (!reg.test(value)) {
              callback(new Error('用途不能是0'))//账号改为用途，已改为输入任意个数任意字符
            } else {
                axios.post("/api/SystemManage/ManagerApi/UserAccountValidate",
                    {
                        userAccount: this.userInfo.userAccount,
                        userId: this.userInfo.userId
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

            }
        };
        let checkEmail = (rule, value, callback) => {
            if (value + "" == "" || value + "" == "undefined" || value + "" == "null") {
                return callback();
            }
            else {
                const regEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if (regEmail.test(value)) {
                    return callback();
                }
                callback(new Error("邮箱格式不正确"));//不用改
            }
        };
        let checkTelephone = (rule, value, callback) => {
            const retPhone = /^(\d{3,4}-)?\d{7,8}$/;
            if (value + "" == "" || value + "" == "undefined" || value + "" == "null") {
                return callback();
            }
            else {
                if (retPhone.test(value)) {
                    return callback();
                }
                callback(new Error("办公电话格式不正确"));//不用改
            }
        };
        return {
            shuriqi:[],
            searchName: "",
            riqi:[],
            total: 1000,
            pageSize: 10,
            userListInfo: [],
            currentPage: 1,
            editFormDialog: false,
            sexRadio: 1,
            userRole: [],
            userInfo: {
                userSex: '1',
                userRoles: []//表格提示区域
            },
            rules: {
                userAccount: [//管理员添加提示区域//账单添加提示区，有提示就是必须有的
                    { required: true, message: '请输入用途', trigger: 'blur' },
                    { validator: validAccout, trigger: 'blur' }
                ],
                userName: [
                    { required: true, message: '请输入姓名', trigger: 'blur' }

                ],
                shuriqi: [
                    { required: true, message: '请输入日期', trigger: 'blur' }
                   // , {  Message: '请照格式输入qqqq-ww-ee', trigger: 'blur' }
                ],
                userSex: [
                    { required: true, message: '请选择类型', trigger: 'change' }
                ],
                //leixing: [
                //    { required: true, message: '请选择类型', trigger: 'change' }
                //],
                userRoles: [
                    { type: 'array', required: true, message: '请选择成员', trigger: 'change' }
                ],
                userMobilePhone: [

                    { required: true, message: '请输入金额', trigger: 'blur' }
                    ,{ min: 0, max: 9999999999999, message: '金额格式错误', trigger: 'blur' }//解除数量限制
                ],
                userEmail: [
                    { validator: checkEmail, trigger: 'blur' }
                ],
                userTelPhone: [
                    { validator: checkTelephone, trigger: 'blur' }
                    //blur应该是整数，change应该是类似枚举
                ]
            }
        };
    },
    methods: {
        //页面功能区域
        search: function (pageIndex) {
            this.count();
            this.currentPage = pageIndex;
            //当把最后一页的数据删完时，页索引调至前一页
            if (this.total % this.pageSize == 0 && parseInt(this.total / this.pageSize) + 1 == pageIndex && pageIndex != 1) {
                pageIndex = pageIndex - 1;
                this.currentPage = pageIndex;
            }
            this.getUserList(pageIndex);
        },
        count: function () {
            //人员个数
            axios.post("/api/SystemManage/ManagerApi/UserCount",
                {
                   // userName: this.searchName,
                    shuriqi:this.riqi1,
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.total = res.userCount;
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




        //管理员信息
        getUserList: function (pageIndex) {
            axios.post("/api/SystemManage/ManagerApi/UserList",
                {
                    //userName: this.searchName,
                    //riqi: this.riqi,
                    shuriqi: this.riqi1,

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
                    this.userListInfo = res.listUserInfo;

                    console.log(this.userListInfo)
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
            //处理当前更改的变化
            this.currentPage = val;
            this.search(this.currentPage);
        },
        //角色下拉菜单
        addEdit: async function (id) {
            await axios.post("/api/SystemManage/ManagerApi/DrpListRole", {},
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                    this.userRole = res.listRoleManage;
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



            ////类型下拉
            //ddEdit: async function (id) {
            //    await axios.post("/api/SystemManage/ManagerApi/leixingxiala", {},
            //        {
            //            headers: {
            //                Authorization: window.localStorage.getItem("storToken")
            //            }
            //        }
            //    ).then(response => {
            //        var res = response.data;
            //        if (res.resultInfo.isSuccess == 1) {
            //            this.userRole = res.leixingxialacaidan;
            //        }
            //        else {
            //            this.$message({
            //                duration: 1000,
            //                type: 'warning',
            //                message: res.resultInfo.errorInfo
            //            });
            //        }
            //    }).catch(function (error) {
            //        // 请求失败处理
            //        console.log(error);
            //    });




            //单条查询
            await axios.post("/api/SystemManage/ManagerApi/UserSingleInfo",
                {
                    userId: id
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                //注意项，需先初始化begin
                this.$nextTick(() => {
                    //this.$refs['userInfo'].resetFields();

                });
                //注意项，需先初始化end
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {
                   
                    this.userInfo = res.userSaveInput;
                    this.userInfo.riqi = res.riqi.riqi;
                    this.userInfo.new = res.riqiNe;
                    console.log('******' + this.userInfo.riqi);
                    this.riqi = res.riqi.riqi;
               
                    this.userInfo.userId = id;
                    
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
       

        //await axios.post("/api/SystemManage/ManagerApi/chafanwei",
        //    {
        //        shuriqi:riqi
        //    },
        //    {
        //        headers: {
        //            Authorization: window.localStorage.getItem("storToken")
        //        }
        //    }
        //).then(response => {
        //    //注意项，需先初始化begin
        //    this.$nextTick(() => {
        //        this.$refs['userInfo'].resetFields();
        //    });
        //    //注意项，需先初始化end
        //    var res = response.data;
        //    if (res.resultInfo.isSuccess == 1) {
        //        this.userInfo = res.userSaveInput;
        //        this.userInfo.userId = id;
        //        this.editFormDialog = true;
        //    }
        //    else {
        //        this.$message({
        //            duration: 1000,
        //            type: 'warning',
        //            message: res.resultInfo.errorInfo
        //        });
        //    }
        //}).catch(function (error) {
        //    // 请求失败处理
        //    console.log(error);
        //});



        },//查询日期范围
        save1: function () {
             axios.post("/api/SystemManage/ManagerApi/chafanwei",
                {
                    userId: id
                },
                {
                    headers: {
                        Authorization: window.localStorage.getItem("storToken")
                    }
                }
            ).then(response => {
                //注意项，需先初始化begin
                this.$nextTick(() => {
                    //this.$refs['userInfo'].resetFields();

                });
                //注意项，需先初始化end
                var res = response.data;
                if (res.resultInfo.isSuccess == 1) {

                    this.userInfo = res.userSaveInput;
                    this.userInfo.riqi = res.riqi.riqi;
                    this.userInfo.new = res.riqiNe;
                    console.log('******' + this.userInfo.riqi);
                    this.riqi = res.riqi.riqi;

                    this.userInfo.userId = id;

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
            ///编辑账单
            var roleInfo = this.userInfo.userRoles;
            this.$refs['userInfo'].validate((valid) => {
                if (valid) {
                    console.log(this.userInfo.riqi)
                    axios.post("/api/SystemManage/ManagerApi/UserSave",
                        {
                            userId: this.userInfo.userId,
                            userAccount: this.userInfo.userAccount,
                            userName: this.userInfo.userName,
                            userSex: this.userInfo.userSex,
                            userTitles: this.userInfo.userTitles,
                            userDept: this.userInfo.userDept,
                            userTelPhone: this.userInfo.userTelPhone,
                            userMobilePhone: this.userInfo.userMobilePhone,
                            userEmail: this.userInfo.userEmail,
                            userRoles: this.userInfo.userRoles,
                            riqi: this.userInfo.riqi
                            /*shuriqi:this.userInfo.shuriqi,*/
                        },
                        {
                            headers: {
                                Authorization: window.localStorage.getItem("storToken")
                            }
                        }
                    ).then(response => {
                        var res = response.data;
                        if (res.resultInfo.isSuccess == 1) {
                            this.$alert('账单编辑成功', '消息', {
                                confirmButtonText: '确定',
                                type:'success',
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
        initPassword: function (id, userName) {
            this.$confirm('确实要初始化【' + userName + '】的密码?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                cancelButtonClass:'btn-custom-cancel',
                type: 'warning'
            }).then(() => {
                //密码初始化
                axios.post("/api/SystemManage/ManagerApi/InitPassWord",
                    {
                        userId: id
                    },
                    {
                        headers: {
                            Authorization: window.localStorage.getItem("storToken")
                        }
                    }
                ).then(response => {
                    var res = response.data;
                    if (res.resultInfo.isSuccess == 1) {
                        this.$message({
                            duration: 1000,
                            type: 'success',
                            message: "初始化成功"
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
                    message: '取消密码初始化'
                });
            });
        },
        delUser: function (id, userName) {
            this.$confirm('确实要删除【' + userName + '】?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                cancelButtonClass: 'btn-custom-cancel',
                type: 'warning'
            }).then(() => {
                //用户删除
                axios.post("/api/SystemManage/ManagerApi/UserDelete",
                    {
                        userId: id
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
                            duration:1000,
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
        }
    },
    created: function () {
        this.search(1);
    }
};

const app = Vue.createApp(App);
app.use(ElementPlus);
app.mount("#app");