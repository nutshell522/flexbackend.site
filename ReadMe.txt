[v]�bflexbackend�إ�EFModels
[v]�إ�Members.dll�A����flexbackend�Ѧҥ�
[v]�bMembers.dll�إߤT�h���[�c��Ƨ�

[-]�|�����U�\��
	-Controllers
	  -MembersController.cs

	-Models
	  -Dtos �ΨӶǻ���ơA���㦳�X�R��k(VM->Dto)
	    RegisterDto.cs
	  
	  -Interfaces �����౵���A�Y�ݭn�s����Ʈw���ӹ�@�o�Ӥ���
	    IMemberRepository.cs

	  -Infra ��@ Interfaces 
		add HashUtility.cs ����K�X (x �|������)
		�ק� web.config, add AppSetting <add key="salt" value="!@#$$DGTEGYT"/>

		add EmailHelper.cs (x �|������)(....)

		sdd Files ��oemail����r��
	    add Result.cs

	   -DapperRepositories
	   -EFRepositories
	     MemberEFRepository.cs
	 
	  -Services �ӷ~�޿�P�_
	    MemberService.cs

	  -ViewsModels �g���ҳW�h
		add RegisterVM.cs , ���U�����ϥΪ�view model
		add MemberController.cs , Register actions

	-Views
	  -Members
	   ComfirmRegister.cshtml �|�����U���\����

[working on]�|���W���`�� MembersList
	-Controllers
	  -MembersController.cs 

	-Models
	  -ViewsModels �g���ҳW�h
		add MembersIndexVM.cs , view model
		add MembersEditVM.cs , view model
		Search function
		Create Edit form �n�[ Dropdown,Checkbox


	  -Dtos �ΨӶǻ���ơA���㦳�X�R��k(Dto->VM)
	    MembersDto.cs

	-Views
	  -Members
	   MembersList.cshtml ����