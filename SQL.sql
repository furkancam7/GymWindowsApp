USE [GymDatabase]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 28.12.2024 01:56:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[admin_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[class_name] [varchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[schedule] [nvarchar](max) NULL,
	[instructor_id] [int] NOT NULL,
	[capacity] [int] NULL,
 CONSTRAINT [PK__Classes__FDF479865C267322] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[enrollment_id] [int] IDENTITY(1,1) NOT NULL,
	[member_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[enrollment_date] [date] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK__Enrollme__6D24AA7A80FD038D] PRIMARY KEY CLUSTERED 
(
	[enrollment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[equipment_id] [int] IDENTITY(1,1) NOT NULL,
	[equipment_name] [varchar](50) NOT NULL,
	[type] [varchar](50) NOT NULL,
	[purchase_date] [date] NULL,
	[condition] [varchar](50) NULL,
 CONSTRAINT [PK__Equipmen__197068AFE4345774] PRIMARY KEY CLUSTERED 
(
	[equipment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment_Rentals]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment_Rentals](
	[rental_id] [int] IDENTITY(1,1) NOT NULL,
	[member_id] [int] NOT NULL,
	[equipment_id] [int] NOT NULL,
	[rental_date] [datetime] NOT NULL,
	[return_date] [datetime] NULL,
	[rental_status] [varchar](20) NULL,
	[notes] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[rental_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maintenance_Logs]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maintenance_Logs](
	[maintenance_id] [int] NOT NULL,
	[staff_id] [int] NOT NULL,
	[equipment_id] [int] NOT NULL,
	[maintenance_date] [date] NULL,
	[notes] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maintenance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[member_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[birth_date] [date] NULL,
	[gender] [nvarchar](10) NULL,
	[phone] [nvarchar](50) NULL,
	[email] [nvarchar](50) NOT NULL,
	[address] [nvarchar](max) NULL,
	[join_date] [date] NULL,
	[membership_plan_id] [int] NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Members__B29B8534E4626305] PRIMARY KEY CLUSTERED 
(
	[member_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Members__AB6E61642AD1482F] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Members__B43B145FFF0AC488] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership_Plans]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership_Plans](
	[membership_plan_id] [int] NOT NULL,
	[plan_name] [varchar](50) NOT NULL,
	[duration] [int] NULL,
	[price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[membership_plan_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Private_Lessons]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Private_Lessons](
	[lesson_id] [int] IDENTITY(1,1) NOT NULL,
	[member_id] [int] NOT NULL,
	[staff_id] [int] NULL,
	[lesson_date] [datetime] NULL,
	[notes] [nvarchar](255) NULL,
 CONSTRAINT [PK__Private___6421F7BE702D55BD] PRIMARY KEY CLUSTERED 
(
	[lesson_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[staff_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[position] [varchar](50) NOT NULL,
	[phone] [varchar](15) NULL,
	[email] [varchar](100) NOT NULL,
	[hire_date] [date] NULL,
	[salary] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Staff__1963DD9C5A381C3F] PRIMARY KEY CLUSTERED 
(
	[staff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Staff__AB6E616439B47F00] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Staff__B43B145FF3C6E6A4] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visits]    Script Date: 28.12.2024 01:56:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visits](
	[visit_id] [int] NOT NULL,
	[member_id] [int] NOT NULL,
	[visit_date] [date] NULL,
	[check_in_time] [time](7) NULL,
	[check_out_time] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[visit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Enrollments] ADD  CONSTRAINT [DF__Enrollmen__enrol__5070F446]  DEFAULT (getdate()) FOR [enrollment_date]
GO
ALTER TABLE [dbo].[Equipment_Rentals] ADD  DEFAULT ('Active') FOR [rental_status]
GO
ALTER TABLE [dbo].[Maintenance_Logs] ADD  DEFAULT (getdate()) FOR [maintenance_date]
GO
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF__Members__join_da__3E52440B]  DEFAULT (getdate()) FOR [join_date]
GO
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_MembershipPlanID]  DEFAULT ((1)) FOR [membership_plan_id]
GO
ALTER TABLE [dbo].[Members] ADD  DEFAULT ('default_password') FOR [password]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF__Staff__hire_date__440B1D61]  DEFAULT (getdate()) FOR [hire_date]
GO
ALTER TABLE [dbo].[Visits] ADD  DEFAULT (getdate()) FOR [visit_date]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK__Classes__instruc__47DBAE45] FOREIGN KEY([instructor_id])
REFERENCES [dbo].[Staff] ([staff_id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK__Classes__instruc__47DBAE45]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK__Enrollmen__class__52593CB8] FOREIGN KEY([class_id])
REFERENCES [dbo].[Classes] ([class_id])
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK__Enrollmen__class__52593CB8]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK__Enrollmen__membe__5165187F] FOREIGN KEY([member_id])
REFERENCES [dbo].[Members] ([member_id])
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK__Enrollmen__membe__5165187F]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollments_Classes] FOREIGN KEY([class_id])
REFERENCES [dbo].[Classes] ([class_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollments_Classes]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollments_Members] FOREIGN KEY([member_id])
REFERENCES [dbo].[Members] ([member_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollments_Members]
GO
ALTER TABLE [dbo].[Equipment_Rentals]  WITH CHECK ADD  CONSTRAINT [FK__Equipment__equip__08B54D69] FOREIGN KEY([equipment_id])
REFERENCES [dbo].[Equipment] ([equipment_id])
GO
ALTER TABLE [dbo].[Equipment_Rentals] CHECK CONSTRAINT [FK__Equipment__equip__08B54D69]
GO
ALTER TABLE [dbo].[Equipment_Rentals]  WITH CHECK ADD FOREIGN KEY([member_id])
REFERENCES [dbo].[Members] ([member_id])
GO
ALTER TABLE [dbo].[Equipment_Rentals]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Rentals_Members] FOREIGN KEY([member_id])
REFERENCES [dbo].[Members] ([member_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Equipment_Rentals] CHECK CONSTRAINT [FK_Equipment_Rentals_Members]
GO
ALTER TABLE [dbo].[Maintenance_Logs]  WITH CHECK ADD  CONSTRAINT [FK__Maintenan__equip__571DF1D5] FOREIGN KEY([equipment_id])
REFERENCES [dbo].[Equipment] ([equipment_id])
GO
ALTER TABLE [dbo].[Maintenance_Logs] CHECK CONSTRAINT [FK__Maintenan__equip__571DF1D5]
GO
ALTER TABLE [dbo].[Maintenance_Logs]  WITH CHECK ADD  CONSTRAINT [FK__Maintenan__staff__5629CD9C] FOREIGN KEY([staff_id])
REFERENCES [dbo].[Staff] ([staff_id])
GO
ALTER TABLE [dbo].[Maintenance_Logs] CHECK CONSTRAINT [FK__Maintenan__staff__5629CD9C]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK__Members__members__3F466844] FOREIGN KEY([membership_plan_id])
REFERENCES [dbo].[Membership_Plans] ([membership_plan_id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK__Members__members__3F466844]
GO
ALTER TABLE [dbo].[Private_Lessons]  WITH CHECK ADD  CONSTRAINT [FK__Private_L__membe__797309D9] FOREIGN KEY([member_id])
REFERENCES [dbo].[Members] ([member_id])
GO
ALTER TABLE [dbo].[Private_Lessons] CHECK CONSTRAINT [FK__Private_L__membe__797309D9]
GO
ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK__Visits__member_i__4D94879B] FOREIGN KEY([member_id])
REFERENCES [dbo].[Members] ([member_id])
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK__Visits__member_i__4D94879B]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [CK__Classes__capacit__46E78A0C] CHECK  (([capacity]>(0)))
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [CK__Classes__capacit__46E78A0C]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [CK__Members__gender__3D5E1FD2] CHECK  (([gender]='Female' OR [gender]='Male'))
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [CK__Members__gender__3D5E1FD2]
GO
ALTER TABLE [dbo].[Membership_Plans]  WITH CHECK ADD CHECK  (([duration]>(0)))
GO
ALTER TABLE [dbo].[Membership_Plans]  WITH CHECK ADD CHECK  (([price]>(0)))
GO
