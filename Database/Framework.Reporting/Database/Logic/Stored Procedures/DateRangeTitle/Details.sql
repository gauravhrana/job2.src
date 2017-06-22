IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleDetails')
BEGIN
  PRINT 'Dropping Procedure DateRangeTitleDetails'
  DROP  Procedure DateRangeTitleDetails
END

GO

PRINT 'Creating Procedure DateRangeTitleDetails'
GO


/******************************************************************************
**		File: 
**		Name: DateRangeTitleDetails
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.DateRangeTitleDetails
(
		@DateRangeTitleId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'DateRangeTitle'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@DateRangeTitleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	DateRangeTitleId			
		,	ApplicationId
		,	Name						
		,	[Description]			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.DateRangeTitle 
	WHERE	DateRangeTitleId		= @DateRangeTitleId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DateRangeTitle'
		,	@EntityKey				= @DateRangeTitleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   