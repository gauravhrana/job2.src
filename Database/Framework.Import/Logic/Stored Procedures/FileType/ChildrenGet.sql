IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeChildrenGet')
BEGIN
	PRINT 'Dropping Procedure FileTypeChildrenGet'
	DROP  Procedure FileTypeChildrenGet
END
GO

PRINT 'Creating Procedure FileTypeChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: FileTypeChildrenGet
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.FileTypeChildrenGet
(
		@FileTypeId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'FileType'
)
AS
BEGIN

	-- GET BatchFile Records
	SELECT	a.BatchFileId
		,	a.ApplicationId	 
		,	a.Name			 
		,	a.Folder
		,	a.BatchFile
		,	a.BatchFileSetId
		,	a.Description
		,	a.FileTypeId
		,	a.SystemEntityTypeId
		,	a.BatchFileStatusId
		,	a.CreatedDate
		,	a.CreatedByPersonId
		,	a.UpdatedDate
		,	a.UpdatedByPersonId
		,	a.Errors
		,	g.Name							AS 'BatchFileSet'
		,	c.EntityName					AS 'SystemEntityType'
		,	b.Name							AS 'FileType'
		,	f.Name							AS 'BatchFileStatus'
	FROM	dbo.BatchFile a
			INNER JOIN dbo.FileType							b		ON		a.FileTypeId			= b.FileTypeId
			INNER JOIN dbo.BatchFileStatus					f		ON		a.BatchFileStatusId		= f.BatchFileStatusId
			INNER JOIN Configuration.dbo.SystemEntityType	c		ON		a.SystemEntityTypeId	= c.SystemEntityTypeId
			INNER JOIN dbo.BatchFileSet						g		ON		a.BatchFileSetId		= g.BatchFileSetId
	WHERE	a.FileTypeId = @FileTypeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   