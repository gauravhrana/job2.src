--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDetails')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserTitleDetails'
--	DROP  Procedure ApplicationUserTitleDetails
--END
--GO

--PRINT 'Creating Procedure ApplicationUserTitleDetails'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleDetails
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

CREATE Procedure dbo.ApplicationUserTitleDetails
(
		@ApplicationUserTitleId		INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME	= NULL	
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationUserTitle'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationUserTitleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	ApplicationUserTitleId
		,	ApplicationId		
		,	Name						
		,	Description			
		,	SortOrder
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'	
	FROM	dbo.ApplicationUserTitle 
	WHERE	ApplicationUserTitleId = @ApplicationUserTitleId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType
		,	@EntityKey						= @ApplicationUserTitleId
		,	@AuditAction					= 'Details'
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   