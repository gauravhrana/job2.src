IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeClone')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeClone'
	DROP  Procedure UserPreferenceDataTypeClone
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeClone
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

CREATE Procedure dbo.UserPreferenceDataTypeClone
(
		@UserPreferenceDataTypeId		INT			= NULL 	OUTPUT	
	,	@ApplicationId					INT	
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR(50)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50) = NULL
)
AS
BEGIN

	IF @UserPreferenceDataTypeId IS NULL OR @UserPreferenceDataTypeId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceDataType', @UserPreferenceDataTypeId OUTPUT
	END			
	
	SELECT	@Name						= Name
		,	@ApplicationId				= ApplicationId
		,	@Description				= Description
		,	@SortOrder					= SortOrder				
	FROM	dbo.UserPreferenceDataType
	WHERE	UserPreferenceDataTypeId	= @UserPreferenceDataTypeId

	EXEC dbo.UserPreferenceDataTypeInsert 
			@UserPreferenceDataTypeId		=	NULL
		,	@ApplicationId					=	@ApplicationId
		,	@Name							=	@Name
		,	@Description					=	@Description
		,	@SortOrder						=	@SortOrder
		,	@AuditId						=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
	     	@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
