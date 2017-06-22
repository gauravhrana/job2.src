IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepClone')
BEGIN
	PRINT 'Dropping Procedure SleepClone'
	DROP  Procedure SleepClone
END
GO

PRINT 'Creating Procedure SleepClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SleepClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.SleepClone
(
		@SleepId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT							
	,	@StudentId				INT								
	,	@Date					DATETIME						
	,	@NapStart				DATETIME						
	,	@NapEnd					DATETIME						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Sleep'				
)

AS

BEGIN

	IF @SleepId IS NULL OR @SleepId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Sleep', @SleepId OUTPUT
	END	
		
	
	SELECT	@StudentId		=	StudentId
		,	@ApplicationId	=	ApplicationId
		,	@Date			=	Date
		,	@NapStart		=	NapStart
		,	@NapEnd			=	NapEnd			
	FROM	dbo.Sleep
	WHERE	SleepId		  = @SleepId
	AND		ApplicationId = @ApplicationId

	EXEC dbo.SleepInsert 
			@SleepId	   = NULL
		,	@ApplicationId = @ApplicationId
		,	@StudentId	   = @StudentId
		,	@Date		   = @Date
		,	@NapStart	   = @NapStart
		,	@NapEnd		   = @NapEnd
		,	@AuditId	   = @AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Sleep'	
		,	@EntityKey				= @SleepId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
