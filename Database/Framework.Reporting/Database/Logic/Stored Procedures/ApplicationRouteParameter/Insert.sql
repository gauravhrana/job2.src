IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteParameterInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteParameterInsert'
	DROP  Procedure ApplicationRouteParameterInsert
END
GO

PRINT 'Creating Procedure ApplicationRouteParameterInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationRouteParameterInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.ApplicationRouteParameterInsert
(
		@ApplicationRouteParameterId			INT				= NULL 	OUTPUT	
	,	@ApplicationRouteId						INT
	,	@ApplicationId								INT			= NULL	
	,	@ParameterName							VARCHAR(100)					
	,	@ParameterValue							VARCHAR(100)	
	,	@AuditId								INT									
	,	@AuditDate								DATETIME		= NULL				
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationRouteParameter'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRouteParameterId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationRouteParameter 
	( 
		ApplicationRouteParameterId	
	,	ApplicationRouteId	
	,	ApplicationId			
	,	ParameterName		
	,	ParameterValue	
	
	)
	VALUES 
	(  
		@ApplicationRouteParameterId	
	,	@ApplicationRouteId	
	,	@ApplicationId							
	,	@ParameterName					
	,	@ParameterValue				
	
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationRouteParameterId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 