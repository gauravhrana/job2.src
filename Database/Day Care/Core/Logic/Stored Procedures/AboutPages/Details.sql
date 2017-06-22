IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesDetails')
BEGIN
	PRINT 'Dropping Procedure AboutPagesDetails'
	DROP  Procedure  AboutPagesDetails
END
GO

PRINT 'Creating Procedure AboutPagesDetails'
GO


/******************************************************************************
**		File: 
**		Name: AboutPagesDetails
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

CREATE Procedure dbo.AboutPagesDetails
(
		@AboutPagesId		INT
	,   @AuditId			INT
	,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'AboutPages'

)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@AboutPagesId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT


	SELECT	AboutPagesId	
		,	ApplicationId					
		,	Description		
		,	Developer
		,	JIRAId
		,	Feature
		,	PrimaryEntity
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'									
	FROM	dbo.AboutPages 
	WHERE	AboutPagesId = @AboutPagesId	

	--Create Audit History
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @AboutPagesId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   