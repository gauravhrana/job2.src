IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationUpdate'
	DROP  Procedure  ApplicationOperationUpdate
END
GO

PRINT 'Creating Procedure ApplicationOperationUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationOperationUpdate
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

CREATE Procedure dbo.ApplicationOperationUpdate
(
		@ApplicationOperationId		INT		 
	,	@ApplicationId				INT			
	,	@Name						VARCHAR(50)			
	,	@OperationValue				VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME	= NULL
	,	@SystemEntityTypeId			INT
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationOperation'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationOperation 
	SET		ApplicationId			=	@ApplicationId
		,	Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder		
		,	OperationValue			=	@OperationValue	
		,	SystemEntityTypeId		=	@SystemEntityTypeId						
	WHERE	ApplicationOperationId	=	@ApplicationOperationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationOperationId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO