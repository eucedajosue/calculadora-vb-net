Public Class CalculadoraForm
    Private parte_entero As String
    Private parte_decimal As String

    Private parte_actual As Integer = 0

    Private Sub Button_digits_Click(sender As Object, e As EventArgs) Handles ButtonPunto.Click,
                                                                               Button0.Click,
                                                                               Button1.Click,
                                                                               Button2.Click,
                                                                               Button3.Click,
                                                                               Button4.Click,
                                                                               Button5.Click,
                                                                               Button6.Click,
                                                                               Button7.Click,
                                                                               Button8.Click,
                                                                               Button9.Click
        Try
            pantalla_agregar(sender.Text)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub pantalla_agregar(digito As String)
        If digito.Equals(".") Then
            If parte_actual <> 0 Then
                Exit Sub
            End If

            parte_actual = 1
        Else
            If parte_actual = 1 Then
                parte_actual = 2
            End If
        End If

        Select Case parte_actual
            Case 0
                parte_entero &= digito
                PantallaTxt.Text = Val(parte_entero).ToString("#,##0")
            Case 1
                PantallaTxt.Text &= digito
            Case 2
                parte_decimal &= digito
                PantallaTxt.Text = Val(parte_entero).ToString("#,##0") & "." & parte_decimal
        End Select
    End Sub
End Class
