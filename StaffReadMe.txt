[v]資料庫版本2
[v]在flexbackend建立EFModels
[v]建立StaffsController
[v]建立Dapper連線字串

[v]員工名單總覽
	add StaffsIndexDto class
	add IStaffRepository interface
	add StaffDapperRepository : IStaffRepository
	add StaffService
	add StaffsController
	add StaffsIndexVM
	add StaffList.cshtml

[v]新增員工 / 註冊員工帳號
	StaffsController add CreateStaff()*2 Get/Post ,傳入StaffsCreateVM
					 通過驗證VM->Dto傳入service
	add IStaffRepository 準備接收StaffService傳入的Dto
	add StaffService 收到Controller傳入的Dto，將資料做檢查，將驗證完畢的Dto傳入IStaffRepository
	add StaffDapperRepository : IStaffRepository 收到 IStaffRepository 傳來的Dto 存入資料庫

	*資料格式還沒有判斷
	*新增成功沒有回到總覽畫面
	*註冊員工帳號是不是要跟新增員工分開

[v]員工登入/登出
	add loginVM
    web.config 在<system.web> add <authentication>
	StaffsController add Login() action , add Login.cshtml , 若沒登入過會自動等向/Staff/Login
					 add Login(LoginVM vm) action 
					 add ValidLogin() method , 對帳號密碼進行驗證,驗證成功並將密碼編碼之後加到 Cookie裡面
					 add ProcessLogin() method , 產生Cookie
	_Layout.cshtml   add 判斷 User.Identity.IsAuthenticated 可以進行變更密碼及登出
	add _LayoutLogin.cshtml 指定給 Login.cshtml 作為版型 

	*沒有將密碼雜湊
	*套用版型以後就會無法登入

[v]忘記密碼
	StaffsController add ForgetPassword() action , add ForgetPassWord.cshtml
					 add ForgetPassword(ForgetPasswordVM vm) action , 呼叫ResetPassword(vm) , 重設新密碼後跳轉回登入頁面
					 add ResetPassword(ForgetPasswordVM vm) , vm->Dto 傳入 service
	add ForgetPasswordVM , 建立重設新密碼畫面
	Service , add ResetPassword(ForgetPasswordDto dto) 驗證帳號是否存在
	IStaffRepository , add SaveNewPassword(string newpassword,string account)
	StaffDapperRepository : IStaffRepository , 傳來的newpassword、account 存入資料庫
	
	*密碼存入資料庫加入雜湊部分


[v]刪除員工
	StaffList.cshtml , <a href="DeleteStaff?staffId=@item.StaffId" class="btn-del"><i class="bi bi-trash-fill"></i></a>
	StaffsController add DeleteStaff(int staffId) action , 檢查是否收到 StaffList.cshtml 傳來的 staffId
	Service , add DeleteStaff(staffId) , 檢查在資料庫是否有相同的一筆員工資料
	IStaffRepository , void DeleteStaff(int staffId)
	StaffDapperRepository : IStaffRepository , DeleteStaff(int staffId)

	*考慮改成接收ajax

[v]變更密碼
[v]查詢員工
	撈出一筆員工

[working on]編輯員工

[-]大頭照