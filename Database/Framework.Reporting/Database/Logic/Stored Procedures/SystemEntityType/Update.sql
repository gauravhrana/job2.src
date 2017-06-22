IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeUpdate'
	DROP  Procedure  SystemEntityTypeUpdate
END
GO

PRINT 'Creating Procedure SystemEntityTypeUpdate'
GO

/******************************************************************************
**		File: 
**		EntityName: SystemEntityTypeUpdate
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
**		Date:		Author:				EntityDescription:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SystemEntityTypeUpdate
(
		@SystemEntityTypeId			INT		 		
	,	@EntityName					VARCHAR(100)			
	,	@EntityDescription			VARCHAR(50)		
	,	@PrimaryDatabase			VARCHAR(50)		
	,	@CreatedDate				DateTime		
	,	@NextValue					INT							
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL						
	,	@SystemEntityType			VARCHAR(50)		= 'SystemEntityType'
)
AS
BEGIN

	UPDATE	dbo.SystemEntityType 
	SET		EntityName			=	@EntityName				
		,	EntityDescription	=	@EntityDescription				
		,	NextValue			=	@NextValue
		,	PrimaryDatabase		= ISNULL(@PrimaryDatabase, 'Configuration')
		,	CreatedDate			= ISNULL(@CreatedDate, GETDATE())								
	WHERE	SystemEntityTypeId	=	@SystemEntityTypeId	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemEntityTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO