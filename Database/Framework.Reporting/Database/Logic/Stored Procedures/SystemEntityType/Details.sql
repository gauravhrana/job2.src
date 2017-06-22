IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeDetails')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeDetails'
	DROP  Procedure SystemEntityTypeDetails
END
GO

PRINT 'Creating Procedure SystemEntityTypeDetails'
GO


/******************************************************************************
**		File: 
**		EntityName: SystemEntityTypeDetails
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

CREATE Procedure dbo.SystemEntityTypeDetails
(
		@SystemEntityTypeId		INT				
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL						
	,	@SystemEntityType			VARCHAR(50)		= 'SystemEntityType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ModuleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	SystemEntityTypeId		
		,	EntityName						
		,	EntityDescription		
		,	NextValue				
		,	IncreaseBy
		,	PrimaryDatabase
		,	CreatedDate	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'			
	FROM	dbo.SystemEntityType 
	WHERE	SystemEntityTypeId = @SystemEntityTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemEntityTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO
   