USE [Flex]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 2023/6/21 下午 10:22:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[CartItemId] [int] IDENTITY(1,1) NOT NULL,
	[fk_CardId] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Description] [nvarchar](700) NOT NULL,
	[Qty] [int] NOT NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[CartItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CouponCategories]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponCategories](
	[CouponCategoryId] [int] NOT NULL,
	[CouponCategoryName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_coupon_category] PRIMARY KEY CLUSTERED 
(
	[CouponCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponId] [int] IDENTITY(1,1) NOT NULL,
	[fk_CouponCategoryId] [int] NOT NULL,
	[CouponName] [nvarchar](50) NOT NULL,
	[CouponCode] [nvarchar](50) NOT NULL,
	[MinimumPurchaseAmount] [int] NOT NULL,
	[DiscountType] [int] NOT NULL,
	[DiscountValue] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndType] [bit] NOT NULL,
	[EndDays] [int] NULL,
	[EndDate] [datetime] NULL,
	[PersonMaxUsage] [int] NULL,
	[RequirementType] [int] NULL,
	[Requirement] [int] NULL,
	[fk_RequiredProjectTagID] [int] NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CouponSendings]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponSendings](
	[SendingId] [int] IDENTITY(1,1) NOT NULL,
	[fk_CouponId] [int] NOT NULL,
	[fk_MemberId] [int] NULL,
	[SentDate] [datetime] NOT NULL,
	[RedemptionStatus] [bit] NOT NULL,
	[RedeemedDate] [datetime] NULL,
 CONSTRAINT [PK_CouponSending] PRIMARY KEY CLUSTERED 
(
	[SendingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[DiscountId] [int] IDENTITY(1,1) NOT NULL,
	[DiscountName] [nvarchar](20) NOT NULL,
	[DiscountDescription] [nvarchar](50) NOT NULL,
	[fk_ProjectTagId] [int] NULL,
	[DiscountType] [int] NOT NULL,
	[DiscountValue] [int] NOT NULL,
	[ConditionType] [int] NOT NULL,
	[ConditionValue] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[DiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTagItems]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTagItems](
	[fk_ProjectTagId] [int] NOT NULL,
	[fk_ProductId] [int] NOT NULL,
 CONSTRAINT [PK_discount_group_item] PRIMARY KEY CLUSTERED 
(
	[fk_ProjectTagId] ASC,
	[fk_ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTags]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTags](
	[ProjectTagId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectTagName] [nvarchar](30) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_discount_group] PRIMARY KEY CLUSTERED 
(
	[ProjectTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCarts]    Script Date: 2023/6/21 下午 10:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCarts](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[fk_MemberID] [int] NOT NULL,
 CONSTRAINT [PK_ShoppingCart] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Coupons] ADD  CONSTRAINT [DF_Coupon_EndDays]  DEFAULT (NULL) FOR [EndDays]
GO
ALTER TABLE [dbo].[Coupons] ADD  CONSTRAINT [DF_Coupon_EndDate]  DEFAULT (NULL) FOR [EndDate]
GO
ALTER TABLE [dbo].[Coupons] ADD  CONSTRAINT [DF_Coupon_PersonMaxUsage]  DEFAULT (NULL) FOR [PersonMaxUsage]
GO
ALTER TABLE [dbo].[Coupons] ADD  CONSTRAINT [DF_Coupon_RequirementType]  DEFAULT (NULL) FOR [RequirementType]
GO
ALTER TABLE [dbo].[Coupons] ADD  CONSTRAINT [DF_Coupon_Requirement]  DEFAULT (NULL) FOR [Requirement]
GO
ALTER TABLE [dbo].[Coupons] ADD  CONSTRAINT [DF_Coupon_fk_RequiredGroupID]  DEFAULT (NULL) FOR [fk_RequiredProjectTagID]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_CartItem] FOREIGN KEY([CartItemId])
REFERENCES [dbo].[ShoppingCarts] ([CartId])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItem_CartItem]
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_CouponCategory] FOREIGN KEY([fk_CouponCategoryId])
REFERENCES [dbo].[CouponCategories] ([CouponCategoryId])
GO
ALTER TABLE [dbo].[Coupons] CHECK CONSTRAINT [FK_Coupon_CouponCategory]
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_ProjectTag] FOREIGN KEY([fk_RequiredProjectTagID])
REFERENCES [dbo].[ProjectTags] ([ProjectTagId])
GO
ALTER TABLE [dbo].[Coupons] CHECK CONSTRAINT [FK_Coupon_ProjectTag]
GO
ALTER TABLE [dbo].[CouponSendings]  WITH CHECK ADD  CONSTRAINT [FK_CouponSending_Coupon] FOREIGN KEY([fk_CouponId])
REFERENCES [dbo].[Coupons] ([CouponId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CouponSendings] CHECK CONSTRAINT [FK_CouponSending_Coupon]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_Discount_DiscountGroup] FOREIGN KEY([fk_ProjectTagId])
REFERENCES [dbo].[ProjectTags] ([ProjectTagId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_Discount_DiscountGroup]
GO
ALTER TABLE [dbo].[ProjectTagItems]  WITH CHECK ADD  CONSTRAINT [FK_discount_group_item_discount_group] FOREIGN KEY([fk_ProjectTagId])
REFERENCES [dbo].[ProjectTags] ([ProjectTagId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectTagItems] CHECK CONSTRAINT [FK_discount_group_item_discount_group]
GO
