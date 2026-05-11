IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tutor] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Subject] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tutor] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NULL,
    [IsTutor] bit NOT NULL,
    [IsStudent] bit NOT NULL,
    [CancellationCount] int NOT NULL,
    [VisibilityScore] float NOT NULL,
    [TutorId] int NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_Tutor_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [Tutor] ([Id])
);
GO

CREATE TABLE [AvailabilitySlots] (
    [Id] int NOT NULL IDENTITY,
    [TutorProfileId] int NOT NULL,
    [DayOfWeek] int NOT NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    [TutorId] int NOT NULL,
    CONSTRAINT [PK_AvailabilitySlots] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AvailabilitySlots_Tutor_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [Tutor] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TutoringOffers] (
    [Id] int NOT NULL IDENTITY,
    [TutorProfileId] int NOT NULL,
    [Subject] nvarchar(max) NOT NULL,
    [Level] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [DurationOptions] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [TutorId] int NOT NULL,
    CONSTRAINT [PK_TutoringOffers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TutoringOffers_Tutor_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [Tutor] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bookings] (
    [Id] int NOT NULL IDENTITY,
    [StudentName] nvarchar(max) NOT NULL,
    [Subject] nvarchar(max) NOT NULL,
    [Date] datetime2 NOT NULL,
    [StudentId] nvarchar(450) NOT NULL,
    [TutorId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_AspNetUsers_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bookings_AspNetUsers_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Reports] (
    [Id] int NOT NULL IDENTITY,
    [ReporterId] nvarchar(450) NOT NULL,
    [ReportedUserId] nvarchar(max) NULL,
    [ReportedReviewId] int NULL,
    [Reason] nvarchar(max) NOT NULL,
    [IsResolved] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reports_AspNetUsers_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [BookingId] int NOT NULL,
    [SenderId] nvarchar(450) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [SentAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Messages_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [BookingId] int NOT NULL,
    [StudentId] nvarchar(450) NOT NULL,
    [TutorId] nvarchar(450) NOT NULL,
    [Rating] int NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsReported] bit NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_AspNetUsers_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reviews_AspNetUsers_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reviews_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE INDEX [IX_AspNetUsers_TutorId] ON [AspNetUsers] ([TutorId]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_AvailabilitySlots_TutorId] ON [AvailabilitySlots] ([TutorId]);
GO

CREATE INDEX [IX_Bookings_StudentId] ON [Bookings] ([StudentId]);
GO

CREATE INDEX [IX_Bookings_TutorId] ON [Bookings] ([TutorId]);
GO

CREATE INDEX [IX_Messages_BookingId] ON [Messages] ([BookingId]);
GO

CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);
GO

CREATE INDEX [IX_Reports_ReporterId] ON [Reports] ([ReporterId]);
GO

CREATE INDEX [IX_Reviews_BookingId] ON [Reviews] ([BookingId]);
GO

CREATE INDEX [IX_Reviews_StudentId] ON [Reviews] ([StudentId]);
GO

CREATE INDEX [IX_Reviews_TutorId] ON [Reviews] ([TutorId]);
GO

CREATE INDEX [IX_TutoringOffers_TutorId] ON [TutoringOffers] ([TutorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260511155713_InitialCreate', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Tutor_TutorId];
GO

ALTER TABLE [AvailabilitySlots] DROP CONSTRAINT [FK_AvailabilitySlots_Tutor_TutorId];
GO

ALTER TABLE [TutoringOffers] DROP CONSTRAINT [FK_TutoringOffers_Tutor_TutorId];
GO

ALTER TABLE [Tutor] DROP CONSTRAINT [PK_Tutor];
GO

EXEC sp_rename N'[Tutor]', N'Tutors';
GO

ALTER TABLE [Bookings] ADD [Status] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Tutors] ADD CONSTRAINT [PK_Tutors] PRIMARY KEY ([Id]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Tutors_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [Tutors] ([Id]);
GO

ALTER TABLE [AvailabilitySlots] ADD CONSTRAINT [FK_AvailabilitySlots_Tutors_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [Tutors] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [TutoringOffers] ADD CONSTRAINT [FK_TutoringOffers_Tutors_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [Tutors] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260511184343_AddTutorsTable', N'8.0.7');
GO

COMMIT;
GO

