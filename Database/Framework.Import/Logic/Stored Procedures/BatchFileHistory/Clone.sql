IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryClone')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryClone'
	DROP  Procedure BatchFileHistoryClone
END
GO

PRINT 'Creating Procedure BatchFileHistoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: BatchFileHistoryClone
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.BatchFileHistoryClone
(
		@BatchFileHistoryId		INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			= NULL
	,	@BatchFileId			INT								
	,	@BatchFileSetId			INT								
	,	@BatchFileStatusId		INT								
	,	@UpdatedDate			DATETIME						
	,	@UpdatedByPersonId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'BatchFileHistory'				
)
AS
BEGIN		
	
	SELECT	@BatchFileId			= BatchFileId	
		,	@ApplicationId			= ApplicationId
		,	@BatchFileSetId			= BatchFileSetId
		,	@BatchFileStatusId		= BatchFileStatusId
		,	@UpdatedDate			= UpdatedDate
		,	@UpdatedByPersonId		= UpdatedByPersonId				
	FROM	dbo.BatchFileHistory
	WHERE	BatchFileHistoryId		= @BatchFileHistoryId

	EXEC dbo.BatchFileHistoryInsert 
			@BatchFileHistoryId		=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@BatchFileId			=	@BatchFileId	
		,	@BatchFileSetId			=	@BatchFileSetId
		,	@BatchFileStatusId		=	@BatchFileStatusId	
		,	@UpdatedDate			=	@UpdatedDate
		,	@UpdatedByPersonId		=	@UpdatedByPersonId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileHistoryId
		,	@AuditAction			= 'Clone'
		,	@UpdatedDate			= @AuditDate
		,	@UpdatedByPersonId		= @AuditId	

END	
GO
