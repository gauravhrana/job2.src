IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryUpdate')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryUpdate'
	DROP  Procedure  BatchFileHistoryUpdate
END
GO

PRINT 'Creating Procedure BatchFileHistoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileHistoryUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.BatchFileHistoryUpdate
(
		@BatchFileHistoryId		INT	 			
	,	@BatchFileId			INT					
	,	@BatchFileSetId			INT					
	,	@BatchFileStatusId		INT					
	,	@UpdatedDate			DATETIME			
	,	@UpdatedByPersonId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileHistory'
)
AS
BEGIN 

	UPDATE	dbo.BatchFileHistory 
	SET		BatchFileId			=	@BatchFileId	
		,	BatchFileSetId		=	@BatchFileSetId		
		,	BatchFileStatusId	=	@BatchFileStatusId		
		,	UpdatedDate			=	@UpdatedDate			
		,	@UpdatedByPersonId	=	@UpdatedByPersonId					
	WHERE	BatchFileHistoryId	=	@BatchFileHistoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BatchFileHistoryId
		,	@AuditAction			= 'Update'
		,	@UpdatedDate			= @AuditDate
		,	@UpdatedByPersonId		= @AuditId
 END		
 GO