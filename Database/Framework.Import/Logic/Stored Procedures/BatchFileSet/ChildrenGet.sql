IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetChildrenGet')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetChildrenGet'
	DROP  Procedure BatchFileSetChildrenGet
END
GO

PRINT 'Creating Procedure BatchFileSetChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileSetChildrenGet
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

CREATE Procedure dbo.BatchFileSetChildrenGet
(
		@BatchFileSetId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'BatchFileSet'
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
	WHERE	a.BatchFileSetId = @BatchFileSetId

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
	WHERE	a.BatchFileSetId = @BatchFileSetId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   