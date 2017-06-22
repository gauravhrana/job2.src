CREATE TABLE dbo.ApplicationUserXApplicationRole(
	ApplicationRoleId					int					NOT NULL,
	ApplicationUserId					int					NULL,
	ApplicationUserXApplicationRoleId	int					NULL,
	ApplicationId						int					NOT NULL
) ON [PRIMARY]
GO