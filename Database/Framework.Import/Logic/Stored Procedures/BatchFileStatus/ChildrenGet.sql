IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusChildrenGet')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusChildrenGet'
	DROP  Procedure BatchFileStatusChildrenGet
END
GO

PRINT 'Creating Procedure BatchFileStatusChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileStatusChildrenGet
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

CREATE Procedure dbo.BatchFileStatusChildrenGet
(
		@BatchFileStatusId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'BatchFileStatus'
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
	WHERE	a.BatchFileStatusId = @BatchFileStatusId

	-- GET BatchFileHistory Records
	SELECT	a.BatchFileHistoryId
		,	a.ApplicationId									
		,	a.BatchFileId											
		,	a.BatchFileSetId										
		,	a.BatchFileStatusId										
		,	a.UpdatedDate											
		,	a.UpdatedByPersonId										
		,	b.Name							AS	'BatchFileSet'						
		,	c.Name							AS	'BatchFileStatus'
	FROM	dbo.BatchFileHistory	a
			INNER JOIN	dbo.BatchFileSet		b		ON		a.BatchFileSetId		= b.BatchFileSetId
			INNER JOIN	dbo.BatchFileStatus		c		ON		a.BatchFileStatusId		= c.BatchFileStatusId	
	WHERE	a.BatchFileStatusId = @BatchFileStatusId	
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   