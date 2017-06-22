IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileChildrenGet')
BEGIN
	PRINT 'Dropping Procedure BatchFileChildrenGet'
	DROP  Procedure BatchFileChildrenGet
END
GO

PRINT 'Creating Procedure BatchFileChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileChildrenGet
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

CREATE Procedure dbo.BatchFileChildrenGet
(
		@BatchFileId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'BatchFile'
)
AS
BEGIN

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
	FROM		dbo.BatchFileHistory	a
	INNER JOIN	dbo.BatchFileSet		b		ON		a.BatchFileSetId		= b.BatchFileSetId
	INNER JOIN	dbo.BatchFileStatus		c		ON		a.BatchFileStatusId		= c.BatchFileStatusId	
	WHERE	a.BatchFileId = @BatchFileId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   