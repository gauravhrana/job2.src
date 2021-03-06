﻿IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='FileTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure FileTypeDoesExist'
	DROP  Procedure  FileTypeDoesExist
END
GO

PRINT 'Creating Procedure FileTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: FileTypeDoesExist
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.FileTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'FileType'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.FileType a
	WHERE		a.Name = @Name	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

