IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileHistoryDoesExist')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryDoesExist'
	DROP  Procedure  BatchFileHistoryDoesExist
END
GO

PRINT 'Creating Procedure BatchFileHistoryDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileHistoryDoesExist
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

Create procedure dbo.BatchFileHistoryDoesExist
(
		@BatchFileId			INT							
	,	@BatchFileSetId			INT							
	,	@BatchFileStatusId		INT							
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFileHistory'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.BatchFileHistory a
	WHERE		a.BatchFileId		= @BatchFileId	
	AND			a.BatchFileSetId	= @BatchFileSetId
	AND			a.BatchFileStatusId = @BatchFileStatusId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@UpdatedDate			= @AuditDate
		,	@UpdatedByPersonId		= @AuditId


END
GO

