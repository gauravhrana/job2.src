IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDelete'
	DROP  Procedure ReleaseLogDelete
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetail_GetDetails')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetail_GetDetails'
	DROP  Procedure ReleaseLogDetail_GetDetails
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailDelete'
	DROP  Procedure ReleaseLogDetailDelete
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailDetails')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailDetails'
	DROP  Procedure ReleaseLogDetailDetails
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailInsert'
	DROP  Procedure ReleaseLogDetailInsert
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailList')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailList'
	DROP  Procedure ReleaseLogDetailList
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetails')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetails'
	DROP  Procedure ReleaseLogDetails
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetails')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetails'
	DROP  Procedure ReleaseLogDetails
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetails_Details')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetails_Details'
	DROP  Procedure ReleaseLogDetails_Details
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetails_GetDetails')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetails_GetDetails'
	DROP  Procedure ReleaseLogDetails_GetDetails
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailSearch'
	DROP  Procedure ReleaseLogDetailSearch
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailsList')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailsList'
	DROP  Procedure ReleaseLogDetailsList
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailUpdate'
	DROP  Procedure ReleaseLogDetailUpdate
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogInsert'
	DROP  Procedure ReleaseLogInsert
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogList')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogList'
	DROP  Procedure ReleaseLogList
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogSearch'
	DROP  Procedure ReleaseLogSearch
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogUpdate'
	DROP  Procedure ReleaseLogUpdate
END
GO