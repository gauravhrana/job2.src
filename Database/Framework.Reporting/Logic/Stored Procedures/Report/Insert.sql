IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportInsert')
BEGIN
	PRINT 'Dropping Procedure ReportInsert'
	DROP  Procedure ReportInsert
END
GO

PRINT 'Creating Procedure ReportInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReportInsert
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.ReportInsert
(
		@ReportId				INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR(500)
	,	@Title					VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Report'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReportId OUTPUT, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@ModifiedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @CreatedDate		= GETDATE() 
	SET @ModifiedDate		= @CreatedDate
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId
	
	INSERT INTO dbo.Report 
	( 
			ReportId
		,	ApplicationId														
		,	Name		
		,	Description
		,	Title			
		,	SortOrder
		,	CreatedDate
		,	ModifiedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId				
	)
	VALUES 
	(  
			@ReportId
		,	@ApplicationId				
		,	@Name			
		,	@Description
		,	@Title		
		,	@SortOrder
		,	@CreatedDate
		,	@ModifiedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId	
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReportId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 