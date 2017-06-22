IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusUpdate')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusUpdate'
	DROP  Procedure  BatchFileStatusUpdate
END
GO

PRINT 'Creating Procedure BatchFileStatusUpdate'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileStatusUpdate
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

CREATE Procedure dbo.BatchFileStatusUpdate
(
		@BatchFileStatusId		INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(50)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileStatus'
)
AS
BEGIN 

	UPDATE	dbo.BatchFileStatus 
	SET		Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	BatchFileStatusId	=	@BatchFileStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO