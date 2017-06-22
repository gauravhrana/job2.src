IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetUpdate')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetUpdate'
	DROP  Procedure  BatchFileSetUpdate
END
GO

PRINT 'Creating Procedure BatchFileSetUpdate'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileSetUpdate
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

CREATE Procedure dbo.BatchFileSetUpdate
(
		@BatchFileSetId			INT	 			
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(50)			
	,	@CreatedDate			DATETIME			
	,	@CreatedByPersonId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileSet'
)
AS
BEGIN 

	UPDATE	dbo.BatchFileSet 
	SET		Name				=	@Name				
		,	Description			=	@Description				
		,	CreatedDate			=	@CreatedDate		
		,	@CreatedByPersonId	=	@CreatedByPersonId					
	WHERE	BatchFileSetId		=	@BatchFileSetId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO