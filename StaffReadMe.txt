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

[v]�s�W���u
	StaffsController add CreateStaff()*2 Get/Post ,�ǤJStaffsCreateVM
					 �q�L����VM->Dto�ǤJservice
	add IStaffRepository �ǳƱ���StaffService�ǤJ��Dto
	add StaffService ����Controller�ǤJ��Dto�A�N��ư��ˬd�A�N���ҧ�����Dto�ǤJIStaffRepository
	add StaffDapperRepository : IStaffRepository ���� IStaffRepository �ǨӪ�Dto �s�J��Ʈw

	*��Ʈ榡�٨S���P�_
	*�s�W���\�^���`���e��

[v]���u�n�J/�n�X
	add loginVM
    web.config �b<system.web> add <authentication>
	StaffsController add Login() action , add Login.cshtml , �Y�S�n�J�L�|�۰ʵ��V/Staff/Login
					 add Login(LoginVM vm) action 
					 add ValidLogin() method , ��b���K�X�i������,���Ҧ��\�ñN�K�X�s�X����[�� Cookie�̭�
					 add ProcessLogin() method , ����Cookie
	_Layout.cshtml   add �P�_ User.Identity.IsAuthenticated �i�H�i���ܧ�K�X�εn�X

	*�S���N�K�X����

[working on]�ѰO�K�X
	StaffsController add ForgetPassword() action , add ForgetPassWord.cshtml

[-]�ק�K�X


[-]�j�Y��