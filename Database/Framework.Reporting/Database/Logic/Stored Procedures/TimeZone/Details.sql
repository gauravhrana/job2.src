IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneDetails')
BEGIN
  PRINT 'Dropping Procedure TimeZoneDetails'
  DROP  Procedure TimeZoneDetails
END

GO

PRINT 'Creating Procedure TimeZoneDetails'
GO


/******************************************************************************
**		File: 
**		Name: TimeZoneDetails
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

CREATE Procedure dbo.TimeZoneDetails
(
		@TimeZoneId					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZone'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TimeZoneId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	TimeZoneId			
		,	ApplicationId
		,	Name						
		,	Description			
		,	SortOrder	
		,	TimeDifference	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.TimeZone 
	WHERE	TimeZoneId = @TimeZoneId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TimeZone'
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   