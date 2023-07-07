-- Discount --
INSERT INTO [dbo].[Discounts] 
([DiscountName], [DiscountDescription], [fk_ProjectTagId], [DiscountType], [DiscountValue], 
[ConditionType], [ConditionValue], [StartDate], [EndDate], [OrderBy])
VALUES
    ('2022�s�~�u�f', '�s�~�u�f', null, 0, 5, 0, 100, '2022-01-01', '2022-01-10', 2),
    ('2022�A��K�`�S�f', '�K�`�S�f', null, 1, 15, 1, 3, '2022-02-15', '2022-03-15', 3),
    ('2022�K�u���u', '�K�`���u', null, 0, 2, 1, 1, '2022-04-01', '2022-05-31', 11),
    ('2022�����S�f', '�����S�f', null, 1, 30, 0, 100, '2022-06-01', '2022-07-31', 6),
    ('2022�}�ǯS�f', '�}�ǯS�|', null, 1, 18, 0, 70, '2022-08-01', '2022-09-30', 14),
    ('2022��V�j�S��', '��V�j�S��', null, 1, 25, 1, 4, '2022-10-01', '2022-12-31', 8),
    ('2023�s�~�u�f', '�s�~�u�f', null, 1, 20, 0, 50, '2023-01-01', '2023-01-30', 4),
    ('2023�K�`�S�f', '�A��K�`���', null, 0, 10, 0, 30, '2023-02-01', '2023-02-27', 9),
    ('9��M��', '9��M��', null, 1, 90, 1, 1, '2023-03-01', NULL, 500),
    ('2��8��', '2��8��', null, 0, 8, 1, 2, '2023-04-10', NULL, 400),
    ('2023�K�L���u�S�f', '���u�S�f', null, 0, 4, 0, 40, '2023-05-01', '2023-05-30', 15),
    ('2023�����S�f', '�����S�f', null, 1, 10, 1, 5, '2023-07-01', '2023-07-30', 1),
    ('2023��կS�f', '��կS�f', null, 0, 3, 1, 10, '2023-08-01', '2023-08-15', 7),
    ('2023�J��S�f', '�J��S�f', null, 1, 5, 0, 20, '2023-09-01', '2023-09-20', 10),
    ('2023�V�u�S��', '�V�u�S��', null, 0, 7, 1, 3, '2023-10-01', '2023-12-31', 100);
go

-- CounponCategories --
INSERT INTO [dbo].[CouponCategories] ([CouponCategoryId], [CouponCategoryName])
VALUES
    (1, '�u�f��'),
    (2, '���U��'),
    (3, '�ͤ��'),
    (4, '�ʪ����y��'),
    (5, '�|�����Ũ�');
go

-- Counpons --
--[DiscountType] 0:���B 1:�ʤ��� 2.�B�O
--[DiscountValue] �inull �ptype��2�A�h�i��null
--[EndType] bit 0:��� 1:�Ѽ�
--[RequirementType] 0���B/ 1���
INSERT INTO [dbo].[Coupons] (
[fk_CouponCategoryId], [CouponName], [CouponCode], [MinimumPurchaseAmount], [DiscountType], 
[DiscountValue], [StartDate], [EndType], [EndDays], [EndDate], 
[PersonMaxUsage], [RequirementType], [Requirement], [fk_RequiredProjectTagID])
VALUES
    (2, '���U§��', Null, 500, 0, 50, '2022-01-01', 1, 30, NULL, 1, 0, NULL, NULL),
    (3, '�ͤ�§��', Null, 200, 1, 15, '2022-02-01', 1, 30, NULL, 2, 0, NULL, NULL),
    (1, '618���ʨ�', Null, 150, 1, 10, '2023-06-18', 0, NULL, '2023-06-30', 1, 0, NULL, NULL),
    (1, '�P�~�y', Null, 250, 0, 20, '2022-04-01', 0, 15, '2023-04-30', 1, 0, NULL, NULL),
    (1, '���H�`§��', Null, 300, 0, 20, '2022-02-07', 0, NULL, '2023-02-17', 2, 0, NULL, NULL),
    (1, '�뤤�K�B��', Null, 180, 2, 0, '2023-06-15', 0, NULL, '2023-06-15', 5, 1, 5, NULL),
    (4, '�n§��', Null, 500, 1, 20, '2023-07-01', 0, NULL, NULL, 3, 0, 1000, 13),
    (1, '�����jFUN�e', Null, 280, 0, 90, '2023-08-01', 0, NULL, '2023-08-15', 2, 0, NULL, NULL),
    (1, 'Criteria�p�W��', Null, 190, 0, 80, '2023-09-01', 0, NULL, '2023-09-30', 1, 0, NULL, NULL),
    (1, '�뤤�K�B��', Null, 350, 1, 20, '2023-10-15', 0, NULL, '2023-10-15', 2, 0, NULL, NULL);
