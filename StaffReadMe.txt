[v]��Ʈw����2
[v]�bflexbackend�إ�EFModels
[v]�إ�StaffsController
[v]�إ�Dapper�s�u�r��

[v]���u�W���`��
	add StaffsIndexDto class
	add IStaffRepository interface
	add StaffDapperRepository : IStaffRepository
	add StaffService
	add StaffsController
	add StaffsIndexVM
	add StaffList.cshtml

[v]�s�W���u / ���U���u�b��
	StaffsController add CreateStaff()*2 Get/Post ,�ǤJStaffsCreateVM
					 �q�L����VM->Dto�ǤJservice
	add IStaffRepository �ǳƱ���StaffService�ǤJ��Dto
	add StaffService ����Controller�ǤJ��Dto�A�N��ư��ˬd�A�N���ҧ�����Dto�ǤJIStaffRepository
	add StaffDapperRepository : IStaffRepository ���� IStaffRepository �ǨӪ�Dto �s�J��Ʈw

	*��Ʈ榡�٨S���P�_
	*�s�W���\�S���^���`���e��
	*���U���u�b���O���O�n��s�W���u���}

[v]���u�n�J/�n�X
	add loginVM
    web.config �b<system.web> add <authentication>
	StaffsController add Login() action , add Login.cshtml , �Y�S�n�J�L�|�۰ʵ��V/Staff/Login
					 add Login(LoginVM vm) action 
					 add ValidLogin() method , ��b���K�X�i������,���Ҧ��\�ñN�K�X�s�X����[�� Cookie�̭�
					 add ProcessLogin() method , ����Cookie
	_Layout.cshtml   add �P�_ User.Identity.IsAuthenticated �i�H�i���ܧ�K�X�εn�X
	add _LayoutLogin.cshtml ���w�� Login.cshtml �@������ 

	*�S���N�K�X����
	*�M�Ϊ����H��N�|�L�k�n�J

[v]�ѰO�K�X
	StaffsController add ForgetPassword() action , add ForgetPassWord.cshtml
					 add ForgetPassword(ForgetPasswordVM vm) action , �I�sResetPassword(vm) , ���]�s�K�X�����^�n�J����
					 add ResetPassword(ForgetPasswordVM vm) , vm->Dto �ǤJ service
	add ForgetPasswordVM , �إ߭��]�s�K�X�e��
	Service , add ResetPassword(ForgetPasswordDto dto) ���ұb���O�_�s�b
	IStaffRepository , add SaveNewPassword(string newpassword,string account)
	StaffDapperRepository : IStaffRepository , �ǨӪ�newpassword�Baccount �s�J��Ʈw
	
	*�K�X�s�J��Ʈw�[�J���곡��


[v]�R�����u
	StaffList.cshtml , <a href="DeleteStaff?staffId=@item.StaffId" class="btn-del"><i class="bi bi-trash-fill"></i></a>
	StaffsController add DeleteStaff(int staffId) action , �ˬd�O�_���� StaffList.cshtml �ǨӪ� staffId
	Service , add DeleteStaff(staffId) , �ˬd�b��Ʈw�O�_���ۦP���@�����u���
	IStaffRepository , void DeleteStaff(int staffId)
	StaffDapperRepository : IStaffRepository , DeleteStaff(int staffId)

	*�Ҽ{�令����ajax

[v]�ܧ�K�X
[v]�d�߭��u
	���X�@�����u

[working on]�s����u

[-]�j�Y��