IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'GetNextTraceId')
BEGIN
	PRINT 'Dropping Procedure GetNextTraceId'
	DROP  Procedure GetNextTraceId
END
GO

PRINT 'Creating Procedure GetNextTraceId'
GO

/*********************************************************************************************
**		File: 
**		Name:GetNextTraceId
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

CREATE Procedure dbo.GetNextTraceId
(
		@TraceId					INT				= NULL 	OUTPUT
	,	@ApplicationId				INT
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'Trace'
)
AS
BEGIN    
	
	INSERT INTO dbo.Trace 
	( 
			ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@ApplicationId	
		,	' ' 						
		,	' '
		,	1
	)

	SELECT @TraceId = SCOPE_IDENTITY()

	UPDATE	dbo.Trace
	SET		Name		=	@TraceId
		,	Description =	@TraceId
		,	SortOrder	=	@TraceId
	WHERE	TraceId		=	@TraceId

	SELECT @TraceId

END	
GO

 