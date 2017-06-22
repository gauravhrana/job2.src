IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeGetNextSequenceTemp')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeGetNextSequenceTemp'
	DROP  Procedure SystemEntityTypeGetNextSequenceTemp
END
GO

PRINT 'Creating Procedure SystemEntityTypeGetNextSequenceTemp'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityTypeGetNextSequenceTemp
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

CREATE PROCEDURE dbo.SystemEntityTypeGetNextSequenceTemp
(
		@SystemEntityTypeId		INT			= NULL
	,	@EntityName				VARCHAR(100) = NULL
	,	@NextValue				INT					OUTPUT
	,	@AuditId				INT			= NULL
)
AS
BEGIN	

	DECLARE @Flag AS INT = 1
	IF @NextValue = -999999 AND @AuditId IS NOT NULL
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
			
			UPDATE	SystemEntityType
			SET		NextValue = NextValue + IncreaseBy
			WHERE	(
						(@SystemEntityTypeId IS NOT NULL AND SystemEntityTypeId = @SystemEntityTypeId)
						OR
						(@SystemEntityTypeId IS NULL AND EntityName = @EntityName)
					)
			
			SELECT	@NextValue = NextValue
			FROM	SystemEntityType
			WHERE	(
						(@SystemEntityTypeId IS NOT NULL AND SystemEntityTypeId = @SystemEntityTypeId)
						OR
						(@SystemEntityTypeId IS NULL AND EntityName = @EntityName)
			)
			
		END
		SELECT @NextValue
END
GO
