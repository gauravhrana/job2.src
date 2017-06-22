
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogClone')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogClone'
	DROP  Procedure  StoredProcedureLogClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDelete')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDelete'
	DROP  Procedure  StoredProcedureLogDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetails')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetails'
	DROP  Procedure  StoredProcedureLogDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDeleteHard')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDeleteHard'
	DROP  Procedure  StoredProcedureLogDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogSearch')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogSearch'
	DROP  Procedure  StoredProcedureLogSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogUpdate')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogUpdate'
	DROP  Procedure  StoredProcedureLogUpdate
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDoesExist')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDoesExist'
	DROP  Procedure  StoredProcedureLogDoesExist
END
GO


IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailClone')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailClone'
	DROP  Procedure  StoredProcedureLogDetailClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailDelete')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailDelete'
	DROP  Procedure  StoredProcedureLogDetailDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailDetails')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailDetails'
	DROP  Procedure  StoredProcedureLogDetailDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailDeleteHard')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailDeleteHard'
	DROP  Procedure  StoredProcedureLogDetailDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailInsert')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailInsert'
	DROP  Procedure  StoredProcedureLogDetailInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailSearch')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailSearch'
	DROP  Procedure  StoredProcedureLogDetailSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailUpdate'
	DROP  Procedure  StoredProcedureLogDetailUpdate
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailDoesExist')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailDoesExist'
	DROP  Procedure  StoredProcedureLogDetailDoesExist
END
GO



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawClone')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawClone'
	DROP  Procedure  StoredProcedureLogRawClone
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawDelete')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawDelete'
	DROP  Procedure  StoredProcedureLogRawDelete
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawDetails')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawDetails'
	DROP  Procedure  StoredProcedureLogRawDetails
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawDeleteHard')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawDeleteHard'
	DROP  Procedure  StoredProcedureLogRawDeleteHard
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawInsert')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawInsert'
	DROP  Procedure  StoredProcedureLogRawInsert
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawSearch')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawSearch'
	DROP  Procedure  StoredProcedureLogRawSearch
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawUpdate')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawUpdate'
	DROP  Procedure  StoredProcedureLogRawUpdate
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawDoesExist')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawDoesExist'
	DROP  Procedure  StoredProcedureLogRawDoesExist
END
GO


