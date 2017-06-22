IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusList')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusList'
	DROP  Procedure  dbo.BatchFileStatusList
END
GO

PRINT 'Creating Procedure BatchFileStatusList'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileStatusList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.BatchFileStatusList
(
		@AuditId				INT		
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileStatus'
)
AS
BEGIN

	SELECT	a.BatchFileStatusId
		,	a.ApplicationId
		,	a.Name
		,	a.Description
		,	a.SortOrder
	FROM	dbo.BatchFileStatus a
	WHERE	a.ApplicationId = @ApplicationId
	ORDER BY a.SortOrder			ASC
		,	 a.Name					ASC
		,	 a.BatchFileStatusId	ASC
		,	 a.ApplicationId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO