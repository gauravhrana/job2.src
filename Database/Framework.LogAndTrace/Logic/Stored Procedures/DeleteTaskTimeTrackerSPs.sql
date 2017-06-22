USE [TaskTimeTracker]

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationClone'
	DROP  Procedure  ApplicationClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationDelete'
	DROP  Procedure  ApplicationDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationDetails'
	DROP  Procedure  ApplicationDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationDeleteHard'
	DROP  Procedure  ApplicationDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationInsert'
	DROP  Procedure  ApplicationInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationSearch'
	DROP  Procedure  ApplicationSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationUpdate'
	DROP  Procedure  ApplicationUpdate
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationDoesExist'
	DROP  Procedure  ApplicationDoesExist
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserClone'
	DROP  Procedure  ApplicationUserClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserDelete'
	DROP  Procedure  ApplicationUserDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserDetails'
	DROP  Procedure  ApplicationUserDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserDeleteHard'
	DROP  Procedure  ApplicationUserDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserInsert'
	DROP  Procedure  ApplicationUserInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserSearch'
	DROP  Procedure  ApplicationUserSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserSearch'
	DROP  Procedure  ApplicationUserSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserSearch'
	DROP  Procedure  ApplicationUserSearch
END
GO


IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleClone'
	DROP  Procedure  ApplicationUserTitleClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleDelete'
	DROP  Procedure  ApplicationUserTitleDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleDetails'
	DROP  Procedure  ApplicationUserTitleDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleDeleteHard'
	DROP  Procedure  ApplicationUserTitleDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleInsert'
	DROP  Procedure  ApplicationUserTitleInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleSearch'
	DROP  Procedure  ApplicationUserTitleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleSearch'
	DROP  Procedure  ApplicationUserTitleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleSearch'
	DROP  Procedure  ApplicationUserTitleSearch
END
GO



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationClone'
	DROP  Procedure  ApplicationOperationClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationDelete'
	DROP  Procedure  ApplicationOperationDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationDetails'
	DROP  Procedure  ApplicationOperationDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationDeleteHard'
	DROP  Procedure  ApplicationOperationDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationInsert'
	DROP  Procedure  ApplicationOperationInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationSearch'
	DROP  Procedure  ApplicationOperationSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationSearch'
	DROP  Procedure  ApplicationOperationSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationSearch'
	DROP  Procedure  ApplicationOperationSearch
END
GO



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleClone'
	DROP  Procedure  ApplicationOperationXApplicationRoleClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleDelete'
	DROP  Procedure  ApplicationOperationXApplicationRoleDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleDetails'
	DROP  Procedure  ApplicationOperationXApplicationRoleDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleDeleteHard'
	DROP  Procedure  ApplicationOperationXApplicationRoleDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleInsert'
	DROP  Procedure  ApplicationOperationXApplicationRoleInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleSearch'
	DROP  Procedure  ApplicationOperationXApplicationRoleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleSearch'
	DROP  Procedure  ApplicationOperationXApplicationRoleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleSearch'
	DROP  Procedure  ApplicationOperationXApplicationRoleSearch
END
GO



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleClone'
	DROP  Procedure  ApplicationUserXApplicationRoleClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleDelete'
	DROP  Procedure  ApplicationUserXApplicationRoleDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleDetails'
	DROP  Procedure  ApplicationUserXApplicationRoleDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleDeleteHard'
	DROP  Procedure  ApplicationUserXApplicationRoleDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleInsert'
	DROP  Procedure  ApplicationUserXApplicationRoleInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleSearch'
	DROP  Procedure  ApplicationUserXApplicationRoleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleSearch'
	DROP  Procedure  ApplicationUserXApplicationRoleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleSearch'
	DROP  Procedure  ApplicationUserXApplicationRoleSearch
END
GO



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleClone'
	DROP  Procedure  ApplicationRoleClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleDelete'
	DROP  Procedure  ApplicationRoleDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleDetails'
	DROP  Procedure  ApplicationRoleDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleDeleteHard'
	DROP  Procedure  ApplicationRoleDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleInsert'
	DROP  Procedure  ApplicationRoleInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleSearch'
	DROP  Procedure  ApplicationRoleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleSearch'
	DROP  Procedure  ApplicationRoleSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleSearch'
	DROP  Procedure  ApplicationRoleSearch
END
GO




