IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteParameterClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteParameterClone'
	DROP  Procedure ApplicationRouteParameterClone
END
GO

PRINT 'Creating Procedure ApplicationRouteParameterClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationRouteParameterClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationRouteParameterClone
(
		@ApplicationRouteParameterId		INT			= NULL 	OUTPUT	
	,	@ApplicationRouteId					INT
	,	@ApplicationId	        INT         = NULL
	,	@ParameterName						VARCHAR(100)					
	,	@ParameterValue						VARCHAR(100)
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRouteParameter'
)
AS
BEGIN

	IF @ApplicationRouteParameterId IS NULL OR @ApplicationRouteParameterId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRouteParameterId OUTPUT
	END						
	
	SELECT	@ApplicationId				=	ApplicationId
		,	@ApplicationRouteId					=	ApplicationRouteId
		,	@ParameterName				=	ParameterName				
		,	@ParameterValue				=	ParameterValue							
		
	FROM	dbo.ApplicationRouteParameter
	WHERE   ApplicationRouteParameterId		=	@ApplicationRouteParameterId
	ORDER BY ApplicationRouteParameterId

	EXEC dbo.ApplicationRouteParameterInsert 
			@ApplicationRouteParameterId		=	NULL
		,   @ApplicationRouteId					=   @ApplicationRouteId
		,   @ApplicationId						=   @ApplicationId
		,	@ParameterName						=	@ParameterName
		,	@ParameterValue						=	@ParameterValue			
		,	@AuditId							=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRouteParameter'
		,	@EntityKey				= @ApplicationRouteParameterId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
