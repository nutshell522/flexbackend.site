INSERT INTO [dbo].[Discounts] ([DiscountName], [DiscountDescription], [fk_ProjectTagId], [DiscountType], [DiscountValue], [ConditionType], [ConditionValue], [StartDate], [EndDate], [OrderBy])
VALUES
    ('Discount 1', 'Description 1', null, 1, 10, 1, 100, '2023-07-01', NULL, 1),
    ('Discount 2', 'Description 2', null, 1, 20, 2, 200, '2023-06-28', NULL, 2),
    ('Discount 3', 'Description 3', null, 1, 30, 1, 300, '2023-06-25', '2023-06-27', 3),
    ('Discount 4', 'Description 4', null, 2, 15, 2, 150, '2023-06-20', '2023-06-29', 4),
    ('Discount 5', 'Description 5', null, 2, 25, 1, 250, '2023-06-15', '2023-06-18', 5);