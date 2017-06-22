using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Shared.UI.WebFramework
{
	/// <summary>
	/// Summary description for DynamicValidationManager.
	/// </summary>

	public class DynamicValidationManager
	{
		public delegate void CustomValidatorServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args);

		#region Private properties

		// These are the validator types that we will cater for
		private enum ValidatorTypes {Common, Compare, Custom, Range, RegularExpression, RequiredField, ValidationSummary};

		// Holds the collection of validator controls defined in the <ValidatorCollections> node of the configuration file
		// This will hold an array of string dictionaries of property/values for each defined validator
		private Hashtable validatorCollections = new Hashtable();

		#endregion 

		#region Public methods		
		/// <summary>
		/// Builds the Dynamic Validation object
		/// </summary>
		/// <param name="validatorsConfigFile">The configuration file for the Dynamic Validator Controls</param>
		public DynamicValidationManager( string validatorsConfigFile )
		{

			try
			{
				// Load the Dynamic Validator Control's configuration file into an XML document
				var configurationDocument = new XmlDocument();
                if (!string.IsNullOrEmpty(validatorsConfigFile))
                {
                    var xmlTextReader = new XmlTextReader(validatorsConfigFile);
                    configurationDocument.Load(xmlTextReader);

                    // Holds the default properties defined in the <Defaults> node of the configuration file
                    // The array will hold one StringDictionary of default properties and values for each type of validator
                    var defaultProperties = new StringDictionary[Enum.GetNames(typeof(ValidatorTypes)).Length];

                    // Loop through each ValidatorType
                    var iCnt = 0;
                    foreach (var validatorType in Enum.GetNames(typeof(ValidatorTypes)))
                    {
                        // Create a new hashtable to hold the property/value pairs for the current validator type
                        defaultProperties[iCnt] = new StringDictionary();

                        // Load the default settings from the configuration document
                        LoadDefaultProperties(configurationDocument, validatorType, defaultProperties[iCnt]);

                        // Increment the counter
                        iCnt++;
                    }

                    // Loads the groups of validator controls defined in the <ValidatorSets> node of the configuration file
                    LoadAllValidatorCollections(configurationDocument, defaultProperties);
                }
			}
			catch( Exception exception ) 
			{
				// Indicate if there was a problem
				throw new Exception( "Unable to instantiate", exception );
			}
		}

		public void LoadDynamicValidators(UserControl userControl)
		{
			for ( int iCnt = 0; iCnt < userControl.Controls.Count; iCnt++ )
			{
				Control childControl = userControl.Controls[iCnt];
				if ( childControl is PlaceHolder )
				{
					LoadDynamicValidators( (PlaceHolder) childControl, userControl );
				}
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Dynamically load validators into a placeholder
		/// </summary>
		/// <param name="placeHolder">The place holder to load the validators into</param>
		/// <param name="userControl">The user control that hosts the user input fields and validation controls</param>
		private void LoadDynamicValidators(PlaceHolder placeHolderControl, UserControl userControl )
		{
			// Get the list of validators to be dynamically added to this userControlChildControl
			var validatorList = (ArrayList) validatorCollections[ placeHolderControl.ID ];

			// Only process controls that have been configured to contain dynamically created validator controls
			if ( validatorList != null )
			{
				// Loop through each validator in the list
				for ( var iCnt = 0; iCnt < validatorList.Count; iCnt++ )
				{
					// Get the string dictionary of property name/values for the validator
					var validatorProperties = (StringDictionary) validatorList[iCnt];

					// Create and add a spacer to go between each dynamically created placeHolderControl
					// Note that whether this is done (and what is added) could be driven from the configuration file
					var spacer = new Literal();
					spacer.Text = "&nbsp;";
					userControl.Controls.Add( spacer );

					// Dynamically create and populate the validator type based on configuration information held in the string dictionary
					switch( validatorProperties["ValidatorType"].ToLower() )
					{
							// Each case statement has the same form:
							//    (1) create the correct type of validator,
							//    (2) set the properties of the validator
							//    (3) add it to the placeHolderControl placeHolderControl
						case "range":
							var rangeValidator = new RangeValidator();
							SetProperties( rangeValidator, validatorProperties );
							placeHolderControl.Controls.Add( rangeValidator );
							break;

						case "requiredfield":
							var requiredFieldValidator = new RequiredFieldValidator();
							SetProperties( requiredFieldValidator, validatorProperties );
							placeHolderControl.Controls.Add( requiredFieldValidator );
							break;

						case "regularexpression":
							var regularExpressionValidator = new RegularExpressionValidator();
							SetProperties( regularExpressionValidator, validatorProperties );
							placeHolderControl.Controls.Add( regularExpressionValidator );
							break;

						//case "validationsummary":
						//    ValidationSummary validationSummary = new ValidationSummary();
						//    SetProperties( validationSummary, validatorProperties );
						//    placeHolderControl.Controls.Add( validationSummary );
						//    break;

						//case "compare":
						//    CompareValidator compareValidator = new CompareValidator();
						//    SetProperties( (Control) compareValidator, validatorProperties );
						//    placeHolderControl.Controls.Add( compareValidator );
						//    break;
				
						//    // Custom validators also need the event handler to be set
						//case "custom":
						//    CustomValidator customValidator = new CustomValidator();
						//    SetProperties( (Control) customValidator, validatorProperties );
						//    SetEventHandler( customValidator, "ServerValidate", validatorProperties["ServerValidate"], userControl );
						//    placeHolderControl.Controls.Add( customValidator );
						//    break;
					}
				}
			}
		}

		/// <summary>
		/// Set a validator control's properties from the name/value pairs found in a string dictionary
		/// </summary>
		/// <param name="control">The control whose properties are to be set</param>
		/// <param name="validatorProperties">The string dictionary of name/value pairs</param>
		private void SetProperties( Control control, StringDictionary validatorProperties )
		{
			// Get the type declaration information for this control
			Type controlType = control.GetType();

			// Will be used to hold the property value
			object propertyValue;

			// Loop through all the property names in the string dictionary of name/value pairs
			foreach ( string propertyName in validatorProperties.Keys )
			{
				// Try and get property info for the given property name
				PropertyInfo propertyInfo = controlType.GetProperty( propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase );
				if ( propertyInfo != null )
				{
					// Assign an object of the correct type and value
					propertyValue = null;
					switch( propertyInfo.PropertyType.ToString() )
					{
						case "System.Boolean":
							propertyValue = Convert.ToBoolean(validatorProperties[ propertyInfo.Name ]);
							break;

						case "System.Int32":
							propertyValue = Convert.ToInt32(validatorProperties[ propertyInfo.Name ]);
							break;

						case "System.String":
							propertyValue = validatorProperties[ propertyInfo.Name ];
							break;

						case "System.Drawing.Color":
							propertyValue = Color.FromName( validatorProperties[ propertyInfo.Name ] );
							break;

						case "System.Web.UI.WebControls.ValidationCompareOperator":
							switch ( validatorProperties[ propertyInfo.Name ].ToLower() )
							{
								case "datatypecheck":
									propertyValue = ValidationCompareOperator.DataTypeCheck;
									break;
								case "equal":
									propertyValue = ValidationCompareOperator.Equal;
									break;
								case "greaterthan":
									propertyValue = ValidationCompareOperator.GreaterThan;
									break;
								case "greaterthanequal":
									propertyValue = ValidationCompareOperator.GreaterThanEqual;
									break;
								case "lessthan":
									propertyValue = ValidationCompareOperator.LessThan;
									break;
								case "lessthanequal":
									propertyValue = ValidationCompareOperator.LessThanEqual;
									break;
								case "notequal":
									propertyValue = ValidationCompareOperator.NotEqual;
									break;
							}
							break;

						case "System.Web.UI.WebControls.ValidationDataType":
							switch ( validatorProperties[ propertyInfo.Name ].ToLower() )
							{
								case "string":
									propertyValue = ValidationDataType.String;
									break;
								case "integer":
									propertyValue = ValidationDataType.Integer;
									break;
								case "double":
									propertyValue = ValidationDataType.Double;
									break;
								case "date":
									propertyValue = ValidationDataType.Date;
									break;
								case "currency":
									propertyValue = ValidationDataType.Currency;
									break;
							}
							break;

						case "System.Web.UI.WebControls.ValidatorDisplay":
							switch ( validatorProperties[ propertyInfo.Name ].ToLower() )
							{
								case "dynamic":
									propertyValue = ValidatorDisplay.Dynamic;
									break;
								case "none":
									propertyValue = ValidatorDisplay.None;
									break;
								case "static":
									propertyValue = ValidatorDisplay.Static;
									break;
							}
							break;
						case "System.Web.UI.WebControls.ValidationSummaryDisplayMode":
							switch ( validatorProperties[ propertyInfo.Name ].ToLower() )
							{
								case "bulletlist":
									propertyValue = ValidationSummaryDisplayMode.BulletList;
									break;
								case "list":
									propertyValue = ValidationSummaryDisplayMode.List;
									break;
								case "singleparagraph":
									propertyValue = ValidationSummaryDisplayMode.SingleParagraph;
									break;
							}
							break;

					}
					propertyInfo.SetValue( control, propertyValue, null );
				}
			}
		}

		/// <summary>
		/// Set an event handler on a validation control to invoke a emthod in the user control
		/// </summary>
		/// <param name="validationControl">The validation control</param>
		/// <param name="eventName">The name of the event</param>
		/// <param name="methodName">The method to invoke</param>
		/// <param name="userControl">The user control on which the method is declared</param>
		private void SetEventHandler( Control validationControl, string eventName, string methodName, UserControl userControl)
		{
			if ( methodName != null && eventName != null )
			{
				// Get the type object for the user control and the child control
				Type childControlType = validationControl.GetType();

				// Get information on the method and the event
				EventInfo eventInfo = childControlType.GetEvent( eventName );

				// Create a delegate of the correct type that will invoke the specified method on the class instance of the user control
				Delegate delegateEventHandler = (Delegate) Delegate.CreateDelegate( eventInfo.EventHandlerType, userControl, methodName);

				// Add the delegate as the eventhandler for the child control
				eventInfo.AddEventHandler( validationControl, delegateEventHandler );
			}
		}
 
		/// <summary>
		/// Loads default settings from the configuration document into a property store
		/// </summary>
		/// <param name="configurationDocument">The XML document that holds the configuration information</param>
		/// <param name="validatorType">The validator type to load</param>
		/// <param name="propertyStore">The store to hold the retrieved default properties and values</param>
		private void LoadDefaultProperties( XmlDocument configurationDocument, string validatorType, StringDictionary defaultPropertiesStore )
		{
			// Select the node that holds the default properties for the specified validator
			XmlNode defaultValidatorNode = configurationDocument.SelectSingleNode( "//Defaults/" + validatorType );

			// If there was a node containing default validator properties
			if ( defaultValidatorNode != null )
			{
				// For each validator property
				foreach( XmlNode defaultValidatorProperty in defaultValidatorNode.ChildNodes )
				{
					// Only process XML elements and ignore comments, etc
					if ( defaultValidatorProperty is XmlElement )
					{
						// Insert the property name and the default value into the store of default properties store
						string propertyName = GetAttribute( defaultValidatorProperty, "name" );
						string propertyValue = GetAttribute( defaultValidatorProperty, "value" );
						defaultPropertiesStore[ propertyName ] = propertyValue;
					}
				}
			}
		}

		/// <summary>
		/// Loads all of the validator collections
		/// </summary>
		/// <param name="configurationDocument">The XML document that holds the configuration information</param>
		private void LoadAllValidatorCollections( XmlDocument configurationDocument, StringDictionary[] defaultProperties )
		{
			// Select the node that holds all of the validator collections for a given user input field
			XmlNode allValidatorCollections = configurationDocument.SelectSingleNode( "//ValidatorSets" );

			// If we got the node that holds the validator collections
			if ( allValidatorCollections != null )
			{
				// Iterate through the validator collections
				foreach ( XmlNode validatorCollection in allValidatorCollections.ChildNodes )
				{
					// Load the validator collection for the user input field
					if ( validatorCollection is XmlElement )
					{
						LoadIndividualValidatorCollection( validatorCollection, defaultProperties );
					}
				}
			}
		}

		/// <summary>
		/// Load a collection of validators to be applied to a given user input field
		/// </summary>
		/// <param name="validatorCollection">The validator collection</param>
		/// <param name="defaultProperties">The default property values</param>
		private void LoadIndividualValidatorCollection( XmlNode validatorCollection, StringDictionary[] defaultProperties )
		{
			// The list of validators to be applied to the given field
			ArrayList validatorList = new ArrayList();

			// Remember the control to validate
			string controlToValidate = GetAttribute( validatorCollection, "ControlToValidate" );

			// Iterate through each validator in the collection
			foreach( XmlNode validatorNode in validatorCollection.ChildNodes )
			{
				// Only process XML elements and ignore comments, etc
				if ( validatorNode is XmlElement )
				{
					// Use a new string dictionary to hold the validator's properties and values
					StringDictionary validatorProperties = new StringDictionary();

					// Remember which control this validator should validate
					validatorProperties[ "ControlToValidate" ] = controlToValidate;

					// Remember the type of validator
					string typeofValidator = GetAttribute( validatorNode, "type" );
					validatorProperties["ValidatorType"] = typeofValidator;

					// Add the ServerValidate event handler (only used on Custom validators)
					validatorProperties[ "ServerValidate" ] = GetAttribute( validatorNode, "ServerValidate" );

					// Assign the default property values common to all validators
					AssignDefaultValues( validatorProperties, defaultProperties[(int) ValidatorTypes.Common] );

					// Assign the default property values specific to this type of validator
					ValidatorTypes validatorType = (ValidatorTypes) Enum.Parse( typeof(ValidatorTypes), typeofValidator );
					AssignDefaultValues( validatorProperties, defaultProperties[(int) validatorType] );

					// Iterate through each property node
					foreach ( XmlNode propertyNode in validatorNode.ChildNodes )
					{
						// Only process XML elements and ignore comments, etc
						if ( propertyNode is XmlElement )
						{
							// Add property names/values explicitly given for this validator
							string propertyName = GetAttribute( propertyNode, "name" );
							string propertyValue = GetAttribute( propertyNode, "value" );
							validatorProperties[ propertyName ] = propertyValue;
						}
					}

					// Now we have the string dictionary, make any fieldname replacements that might have been specified
					ReplaceFieldnamesWithValues( validatorProperties, validatorCollection );

					// Finally, add the string dictionary containing the validator property values to the list of validators for this group
					validatorList.Add( validatorProperties );
				}
			}

			// Save the array list of validators for this group
			string collectionName = GetAttribute( validatorCollection, "id" );
			validatorCollections[ collectionName ] = validatorList;
		}

		/// <summary>
		/// Gets the value of an attribute on an XML node
		/// </summary>
		/// <param name="xmlNode">The XML node</param>
		/// <param name="attributeName">The attribute's name</param>
		/// <returns>The attribute's value or String.Empty if the attribute was not defined on the XML node</returns>
		private string GetAttribute( XmlNode xmlNode, string attributeName )
		{
			// This will be our return value
			string attributeValue = String.Empty;

			// Try to get the XML attribute for the specified name
			XmlAttribute xmlAttribute = xmlNode.Attributes[attributeName];

			// If we got one, set the return attribute value
			if ( xmlAttribute != null )
			{
				attributeValue = xmlAttribute.InnerText;
			}

			// Return the attribute value (or String.Empty if there was no such attribute on the XML node)
			return (attributeValue);
		}

		/// <summary>
		/// Assigns default values to the string dictionary of validator properties
		/// </summary>
		/// <param name="validatorProperties">The dictionary of validator properties</param>
		/// <param name="defaultProperties">The default properties store for the given validator type</param>
		private void AssignDefaultValues( StringDictionary validatorProperties, StringDictionary defaultProperties )
		{
			//  Iterate through each key in the default properties store
			foreach ( string propertyName in defaultProperties.Keys )
			{
				// Add the property name and its value into the string dictionary of validator properties
				validatorProperties[ propertyName ] = (string) defaultProperties[ propertyName ];
			}
		}

		/// <summary>
		/// Replace field names with their values
		/// </summary>
		/// <param name="validatorProperties">The properties and values for the validator</param>
		/// <param name="validatorCollection">Holds the field names and values as a series of XML attributes</param>
		private void ReplaceFieldnamesWithValues( StringDictionary validatorProperties, XmlNode validatorCollection )
		{
			string propertyNames = "Text;ErrorMessage";
			string propertyValue;
			string fieldName;
			string replacementText;
			int openBracePosition;
			int closeBracePosition;
			XmlAttribute fieldnameAttribute;
				
			// Iterate through the properties that need to have replacements
			// Field names are of the form "{FIELD_NAME}"
			// The validatorCollection replacement text is the innerText value of an XML attribute of the validatorCollection with the same FIELD_NAME
			foreach( string propertyName in propertyNames.Split( ';' ) )
			{
				// Get the property value
				propertyValue = validatorProperties[ propertyName ];
				
				if ( propertyValue != null )
				{
					// Get the open and close brace positions
					openBracePosition = propertyValue.IndexOf( "{" );
					closeBracePosition = propertyValue.IndexOf( "}" );

					// Loop while there are open and close brace positions within the property value
					int safetyBreakoutCounter = 0;
					while ( openBracePosition != -1 && closeBracePosition != -1 )
					{
						// If the open brace came before the close brace
						if ( openBracePosition < closeBracePosition )
						{
							// Get the field name from the validator property
							fieldName = propertyValue.Substring( openBracePosition + 1, closeBracePosition-openBracePosition-1 );

							// Try to get the XML attribute that holds the replacement text for the field name
							fieldnameAttribute = validatorCollection.Attributes[fieldName];
							if ( fieldnameAttribute != null )
							{
								// Get the replacement text from the attribute
								replacementText = fieldnameAttribute.InnerText;
							}
							else
							{
								// As there is no replacement text, use the validator collection name instead
								replacementText = validatorCollection.Name;
							}

							// Perform the replacement
							propertyValue = propertyValue.Replace( "{" + fieldName + "}", replacementText );
						}

						// See if we can find another open and close brace positions within the validator property
						openBracePosition = propertyValue.IndexOf( "{" );
						closeBracePosition = propertyValue.IndexOf( "}" );

						// This check will enable us to breakout of we've done more than a 100 loops
						// At most, we might expect a few field names to be replaced, so if we've gone round a 100 times, it's a pretty good sign that something is not right
						if ( safetyBreakoutCounter++ > 100 )
						{
							// Although we just break out, we could also throw an error
							break;
						}

					}
					// Remember the potentially modified validator property
					validatorProperties[ propertyName ] = propertyValue;
				}
			}
		}

		#endregion
	}
}
