IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeGetNextSequence')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeGetNextSequence'
	DROP  Procedure dbo.SystemEntityTypeGetNextSequence
END
GO

PRINT 'Creating Procedure SystemEntityTypeGetNextSequence'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityTypeGetNextSequence
**		Desc: Thisd procedure is to create next SystemEntityTypeId.
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
**		Date: 09-22-05
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityName:
**		--------	--------			--------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE dbo.SystemEntityTypeGetNextSequence
(
		@SystemEntityTypeId		INT			= NULL
	,	@EntityName				VARCHAR(100) = NULL
	,	@NextValue				INT					OUTPUT
	,	@AuditId				INT			= NULL
)
AS
BEGIN	

	DECLARE @Flag AS INT 
	SET @Flag = 1


	-- Generate  Next Sequence based on Developer's range only when @NextValue = -999999 (indicated that application is in DEBUG mode) 
	IF @NextValue IS NOT NULL AND @NextValue = -999999 AND @AuditId IS NOT NULL
	BEGIN
		
		SET @Flag = 2
		EXEC	dbo.GetNextSequencePerson 
				@PersonId	=	@AuditId
			,	@EntityName =   @EntityName
			,	@NextValue	=	@NextValue	OUTPUT
			
		IF @NextValue IS NULL
			SET @Flag = 1
	END
	
	IF  @Flag = 1
	BEGIN
		IF @NextValue IS NULL
		BEGIN
		UPDATE	a
		SET		NextValue = NextValue + IncreaseBy
		FROM	dbo.SystemEntityType a
		WHERE	
			(
				(@SystemEntityTypeId IS NOT NULL AND SystemEntityTypeId = @SystemEntityTypeId)
				OR
				(@SystemEntityTypeId IS NULL AND EntityName = @EntityName)
			)
			
		SELECT	@NextValue = NextValue
		FROM	dbo.SystemEntityType
		WHERE	
			(
				(@SystemEntityTypeId IS NOT NULL AND SystemEntityTypeId = @SystemEntityTypeId)
				OR
				(@SystemEntityTypeId IS NULL AND EntityName = @EntityName)
			)
		END
			
	END 
END
GO


/*
DECLARE @NextValue INT

EXECUTE dbo.SystemEntityTypeGetNextSequence 
			NULL
	,		'Person'
	,		@NextValue OUT

SELECT @NextValue


SELECT * FROM SystemEntityType

SELECT "@NextValue" = @NextValue
*/