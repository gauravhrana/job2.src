IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteParameterDetails')
BEGIN
  PRINT 'Dropping Procedure ApplicationRouteParameterDetails'
  DROP  Procedure ApplicationRouteParameterDetails
END

GO

PRINT 'Creating Procedure ApplicationRouteParameterDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationRouteParameterDetails
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

CREATE Procedure dbo.ApplicationRouteParameterDetails
(
		@ApplicationRouteParameterId	INT								
	,	@AuditId						INT					
	,	@AuditDate						DATETIME		= NULL	
	,	@SystemEntityType				VARCHAR(50)		= 'ApplicationRouteParameter'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationRouteParameterId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.ApplicationRouteParameterId			
		,	a.ApplicationRouteId		
		,	a.ApplicationId								
		,	a.ParameterName					
		,	a.ParameterValue
		,	b.RouteName AS 'ApplicationRoute'				
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.ApplicationRouteParameter a
	INNER JOIN dbo.ApplicationRoute b on a.ApplicationRouteId=b.ApplicationRouteId	
	WHERE	ApplicationRouteParameterId = @ApplicationRouteParameterId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRouteParameter'
		,	@EntityKey				= @ApplicationRouteParameterId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   