--1.黑名單
INSERT INTO BlackLists 
(Behavior) 
VALUES 
('詐欺行為'),('未經授權的行為'),('違反使用條款');

SELECT * FROM BlackLists;
--------------------------------------------------
--2.特權
INSERT INTO Privileges 
(PrivilegeName,[Description]) 
VALUES 
('生日優惠','根據會員的生日提供獨家優惠和禮物'),
('專屬活動','邀請特定等級的會員參加線上或線下的活動'),
('訪問新品','新品推出優先讓VVIP會員購買'),
('限量商品','限量商品僅限VVIP會員購買');

SELECT * FROM Privileges;
--------------------------------------------------
--3.等級
INSERT INTO MembershipLevels 
(LevelName,MinSpending,Discount,[Description]) 
VALUES 
('一般會員',0,0,'新註冊的會員自動成為基本級別會員'),
('VIP',8888,88,'消費門檻達8,888即可升級'),
('VVIP',12000,85,'消費門檻達12,000即可升級');

SELECT * FROM MembershipLevels;
--------------------------------------------------
--4.會員等級與特權中介資料表
INSERT INTO MembershipLevelPrivileges (fk_LevelId, fk_PrivilegeId)
VALUES
  (1, 1), -- 一般會員的生日優惠
  (2, 1), -- VIP會員的生日優惠
  (2, 2), -- VIP會員的專屬活動優惠
  (3, 1), -- VVIP會員的生日優惠
  (3, 2), -- VVIP會員的專屬活動優惠
  (3, 3), -- VVIP會員的訪問新品優惠
  (3, 4); -- VVIP會員的限量商品優惠

SELECT * FROM MembershipLevels;
SELECT * FROM Privileges;

SELECT * FROM MembershipLevelPrivileges;

--不同等級的會員享有的福利
SELECT * 
FROM Privileges
JOIN MembershipLevelPrivileges
ON Privileges.PrivilegeId=MembershipLevelPrivileges.fk_PrivilegeId;
--------------------------------------------------
--5.會員
INSERT INTO Members ([Name], Age, Gender, Mobile, Email, Birthday, CommonAddress, Account, EncryptedPassword, IsConfirmed, ConfirmCode, fk_LevelId, fk_BlackListId)
VALUES
  ('John Doe', 25, 1, '0934567890', 'johndoe@example.com', '1997-05-10', '新北市土城區學府路一段6號', 'johndoe123', '123', 1, 'Confirmed', 1, NULL),
  ('Jane Smith', 30, 0, '0976543210', 'janesmith@example.com', '1992-08-15', '新北市新莊區南雅路354號', 'janesmith456', '456', 1, 'Confirmed', 2, 1),
  ('Michael Johnson', 35, 1, '0958889999', 'michaeljohnson@example.com', '1987-03-20', '台中市太平區中正路一段111號', 'mjohnson789', '789', 1, 'Pending', 2, NULL),
  ('Emily Davis', 28, 0, '0972221111', 'emilydavis@example.com', '1993-11-05', '新北市鶯歌區鶯桃路三段7號', 'emilyd321', '444', 1, 'Confirmed', 1, NULL),
  ('David Wilson', 32, 1, '0947778888', 'davidwilson@example.com', '1989-07-12', '台東縣東河鄉濱海路33號', 'dwilson567', '555', 1, 'Confirmed', 1,NULL);

SELECT * FROM Members;


--[會員名單總覽],還要加上累計積分
SELECT MemberId,[Name],Gender,Email,LevelName,Registration,Behavior 
FROM Members
JOIN MembershipLevels ON fk_LevelId=MembershipLevels.LevelId
LEFT JOIN BlackLists ON fk_BlackListId=BlackLists.BlackListId;

--JOIN Members & MembershipLevel & BlackList 
--找出是黑名單的會員
SELECT * 
FROM Members
JOIN MembershipLevels ON fk_LevelId=MembershipLevels.LevelId
JOIN BlackLists ON fk_BlackListId=BlackLists.BlackListId;

--會員資料管理資訊
SELECT MemberId,[Name],Age,Gender,Email,Birthday,Registration,LevelName,Behavior 
FROM Members
JOIN MembershipLevels ON fk_LevelId=MembershipLevels.LevelId
LEFT JOIN BlackLists ON fk_BlackListId=BlackLists.BlackListId;
--------------------------------------------------
--6.備用地址
INSERT INTO AlternateAddresses (AlternateAddress1, AlternateAddress2, fk_MemberId)
VALUES
  ('新北市新莊區南雅路354號',null ,1),
  (NULL, NULL,2),
  ('新北市板橋區府中路一段6號', null,3),
  ('桃園市中壢區聖德路三段7號', NULL,5),
  ('台南市白河鎮白河路354號', '新北市土城區學士路一段36號',4);

SELECT * FROM AlternateAddresses;

--Members & AlternateAddresses
SELECT * FROM 
Members
JOIN AlternateAddresses
ON Members.MemberId=AlternateAddresses.fk_MemberId;

SELECT MemberId,[Name],CommonAddress,AlternateAddress1,AlternateAddress2 FROM 
Members
JOIN AlternateAddresses
ON Members.MemberId=AlternateAddresses.fk_MemberId;
--------------------------------------------------
--9.類型
INSERT INTO [Type] (TypeName)
VALUES
('商品'),
('活動'),
('課程'),
('客製化商品');

SELECT * FROM [Type];
--------------------------------------------------
--7.歷史積分,需要與訂單建立關聯
INSERT INTO PointHistories (GetOrDeduct, UsageAmount,fk_MemberId,fk_TypeId)
VALUES
  (1,100,1,1),
  (0,50,2,2),
  (0,200,3,3),
  (0,30,4,4),
  (1,0,5,1);

SELECT * FROM PointHistories;
--------------------------------------------------
--8.會員積分
INSERT INTO MemberPoints (PointSubtotal, fk_PointHistoryId, fk_MemberId)
VALUES (110, 1, 1),
       (60, 2, 2),
       (420, 3, 5),
       (21, 4, 4),
       (70, 5, 3);

SELECT * FROM MemberPoints;
--------------------------------------------------
--10.積分管理
INSERT INTO PointManage (GetOrDeduct,Amount,fk_TypeId,PointExpirationDate)
VALUES
(1,200,1,'2023-6-27'),(0,100,2,'2023-6-25'),(1,200,2,'2023-6-25');

SELECT * FROM PointManage;
--------------------------------------------------
--11.積分折抵
INSERT INTO PointTradeIn (GiftThreshold,GetPoints,MaxGetPoints,EffectiveDate,ExpirationDate,fk_MemberId)
VALUES
(100,1,1000,'2023-6-27','2024-6-27',1);

SELECT * FROM PointTradeIn
--------------------------------------------------
--------------------------------------------------
--員工權限
INSERT INTO StaffPermissions (LevelName)
VALUES
  ('一般權限'),
  ('中級權限'),
  ('高級權限');

SELECT * FROM StaffPermissions;
--------------------------------------------------
--員工職稱
INSERT INTO JobTitles (TitleName,fk_StaffPermissions)
VALUES ('經理',3),
       ('主管',3),
       ('專員',2),
       ('助理',1),
       ('工讀生',1);

SELECT * FROM JobTitles;

SELECT * 
FROM JobTitles
JOIN StaffPermissions
ON JobTitles.fk_StaffPermissions=StaffPermissions.PermissionsId;
--------------------------------------------------
--部門
INSERT INTO Departments (DepartmentName)
VALUES ('行銷部門'),
       ('技術部門'),
       ('銷售部門'),
       ('商品部門'),
       ('客服部門');

SELECT * FROM Departments;
--------------------------------------------------
--員工名單
INSERT INTO Staffs ([Name], Age, Gender, Mobile, Email, Birthday, Account, [Password], fk_PermissionsId,fk_TitleId,fk_DepartmentId)
VALUES
  ('John Doe', 25, 1, '0912345678', 'john.doe@example.com', '1998-05-15', 'johndoe', '123', 1,1,1),
  ('Jane Smith', 30, 0, '0987654321', 'jane.smith@example.com', '1991-09-20', 'janesmith', '456', 1,3,2),
  ('David Lee', 35, 1, '0922334455', 'david.lee@example.com', '1988-12-10', 'davidlee', '789', 2,1,2),
  ('Sarah Wang', 28, 0, '0933445566', 'sarah.wang@example.com', '1993-06-25', 'sarahwang', 'abc', 3,5,4),
  ('Michael Chen', 32, 1, '0977888999', 'michael.chen@example.com', '1989-04-05', 'michaelchen', 'xyz', 1,4,3);

SELECT * FROM Staffs;

SELECT * FROM Staffs
JOIN Departments
ON Staffs.fk_DepartmentId=Departments.DepartmentId;