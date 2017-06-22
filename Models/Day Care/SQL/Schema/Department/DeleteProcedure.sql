IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='DepartmentDelete') 
	BEGIN
	DROP Procedure DepartmentDelete
END
GO

CREATE Procedure dbo.DepartmentDelete
(
		@DepartmentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Department'
)
AS
BEGIN
		DELETE dbo.Department
		WHERE	DepartmentId = @DepartmentId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @DepartmentId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
